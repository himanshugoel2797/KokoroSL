using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kokoro.KSL.Lib.Math;
using Kokoro.KSL.Lib.Texture;
using Kokoro.KSL.Lib.General;
using Kokoro.KSL.Lib;

#if PC
using CodeGenLL = Kokoro.KSL.GLSL.PC.PCCodeGenerator;
#endif

namespace Kokoro.KSL.GLSL
{
    partial class GLSLCodeGenerator
    {
        static string src;
        static StringBuilder strBuilder;
        static Dictionary<string, string> PreDefinedVariablesMap = new Dictionary<string, string>();
        

        //Generate code from the syntax tree created from its execution
        internal static string CompileFromSyntaxTree(KSL.KSLCompiler.KShaderType shaderType)
        {
            
            //Map some predefined variables for GLSL
            PreDefinedVariablesMap["VertexPosition"] = "gl_Position";
            PreDefinedVariablesMap["VertexID"] = "gl_VertexID";
            PreDefinedVariablesMap["InstanceID"] = "gl_InstanceID";
            PreDefinedVariablesMap["FragCoord"] = "gl_FragCoord";


			//Specify version as per Logic.AvailableSM
			switch(KSL.Lib.General.Logic.AvailableSM){

			case ShadingModel.SM4:
				src = "#version 440 core\n";    //Use attribute to specify version number?
				break;
			case ShadingModel.SM3:
				src = "#version 330 core\n";
				break;
			case ShadingModel.SM2:
				src = "#version 200 core\n";
				break;
			case ShadingModel.SM1:
				src = "";		//TODO look up about whether #version was even available back then
				break;
			}
            strBuilder = new StringBuilder(src);

            //Get variable declarations from the syntax tree and organize them
            List<SyntaxTree.Variable> StreamVars = new List<SyntaxTree.Variable>();
            List<SyntaxTree.Variable> UniformVars = new List<SyntaxTree.Variable>();
            List<SyntaxTree.Variable> SharedVars = new List<SyntaxTree.Variable>();

            foreach (KeyValuePair<string, SyntaxTree.Variable> pair in SyntaxTree.Parameters)
            {
                if (pair.Value.paramType == SyntaxTree.ParameterType.StreamIn || pair.Value.paramType == SyntaxTree.ParameterType.StreamOut)
                {
                    StreamVars.Add(pair.Value);
                }
                else if (pair.Value.paramType == SyntaxTree.ParameterType.Uniform)
                {
                    UniformVars.Add(pair.Value);
                }
                else if (pair.Value.paramType == SyntaxTree.ParameterType.SharedIn || pair.Value.paramType == SyntaxTree.ParameterType.SharedOut)
                {
                    SharedVars.Add(pair.Value);
                }
            }


            //Generate code for the stream variables
            foreach (SyntaxTree.Variable variable in StreamVars)
            {
                strBuilder.AppendFormat("layout(location = {0}) {1} {2} {3};\n",
                    ((int)variable.value).ToString(),
                    ((variable.paramType == SyntaxTree.ParameterType.StreamOut) ? "out" : "in"),
                    ConvertType(variable.type), variable.name);
            }

            strBuilder.AppendLine();

            //Generate code for the uniform variables
            foreach (SyntaxTree.Variable variable in UniformVars)
            {
                strBuilder.AppendFormat("uniform {0} {1};\n",
                    ConvertType(variable.type),
                    variable.name);
            }

            strBuilder.AppendLine();

            //Generate code for the shader variables
            foreach (SyntaxTree.Variable variable in SharedVars)
            {
                strBuilder.AppendFormat("{0} {1} {2};\n",
                    ((variable.paramType == SyntaxTree.ParameterType.SharedOut) ? "out" : "in"),
                    ConvertType(variable.type),
                    variable.name);
            }

            //Generate the main method signature
            strBuilder.AppendLine();
            strBuilder.AppendLine("void main(){");

            //Build the code body using the fundamental operations available to the language
            while (SyntaxTree.Instructions.Count >= 1)
            {
                //Start going through Instruction queue to generate shader body
                SyntaxTree.Instruction instruction = SyntaxTree.Instructions.Dequeue();

                switch (instruction.instructionType)
                {
                    case SyntaxTree.InstructionType.Assign:
                        if (SyntaxTree.Variables.ContainsKey(instruction.Parameters[1]) && SyntaxTree.Variables[instruction.Parameters[0]].type == SyntaxTree.Variables[instruction.Parameters[1]].type)
                        {
                            strBuilder.AppendFormat("{0} = {1};\n", SubstitutePredefinedVars(instruction.Parameters[0]), SubstitutePredefinedVars(instruction.Parameters[1]));
                        }
                        else if (SyntaxTree.Variables.ContainsKey(instruction.Parameters[1]) && SyntaxTree.Variables[instruction.Parameters[0]].type != SyntaxTree.Variables[instruction.Parameters[1]].type)
                        {
                            strBuilder.AppendFormat("{0} = {1}({2});\n", SubstitutePredefinedVars(instruction.Parameters[0]), ConvertType(SyntaxTree.Variables[instruction.Parameters[0]].type), SubstitutePredefinedVars(instruction.Parameters[1]));
                        }
                        else
                        {
                            strBuilder.AppendFormat("{0} = {1};\n", SubstitutePredefinedVars(instruction.Parameters[0]), SubstitutePredefinedVars(instruction.Parameters[1]));
                        }
                        break;

                    case SyntaxTree.InstructionType.Create:
                        SyntaxTree.Variable VAR = SyntaxTree.Variables[instruction.Parameters[0]];
                        strBuilder.AppendFormat("{0} {1};\n", ConvertType(VAR.type), VAR.name);
                        break;
                }
            }

            strBuilder.AppendLine("}");

            return strBuilder.ToString();
        }

        internal static string GetShader()
        {
            return src;
        }


        static string currentDeclaration = "";

        //Translate KSL function calls to GLSL equivalents
        internal static string TranslateSDKFunctionCalls(SyntaxTree.FunctionCalls function, params string[] parameters)
        {
            string str = "";

            switch (function)
            {
                case SyntaxTree.FunctionCalls.Tex3D:
                    str = "texture3D(" + parameters[0] + "," + parameters[1] + ")";
                    break;
                case SyntaxTree.FunctionCalls.Tex2D:
                    str = "texture2D(" + parameters[0] + ", " + parameters[1] + ")";
                    break;
                case SyntaxTree.FunctionCalls.Tex1D:
                    str = "texture1D(" + parameters[0] + ", " + parameters[1] + ")";
                    break;

                case SyntaxTree.FunctionCalls.Cross2D:
                    str = "cross(" + parameters[0] + ", " + parameters[1] + ")";
                    break;
                case SyntaxTree.FunctionCalls.Cross3D:
                    str = "cross(" + parameters[0] + ", " + parameters[1] + ")";
                    break;
                case SyntaxTree.FunctionCalls.Cross4D:
                    str = "cross(" + parameters[0] + ", " + parameters[1] + ")";
                    break;

                case SyntaxTree.FunctionCalls.Normalize2D:
                    str = "normalize(" + parameters[0] + ")";
                    break;
                case SyntaxTree.FunctionCalls.Normalize3D:
                    str = "normalize(" + parameters[0] + ")";
                    break;
                case SyntaxTree.FunctionCalls.Normalize4D:
                    str = "normalize(" + parameters[0] + ")";
                    break;

                case SyntaxTree.FunctionCalls.Mod:
                    str = "mod(" + parameters[0] + ", " + parameters[1] + ")";
                    break;
            }

            return str;
        }

        //Substitute reserved predefined variables with the GLSL specifc name
        internal static string SubstitutePredefinedVars(string varName)
        {
            foreach(KeyValuePair<string, string> substitutions in PreDefinedVariablesMap)
            {
                varName = varName.Replace(substitutions.Key, substitutions.Value);
            }

            return varName;
        }

        //Convert the C# Type object to its equivalent GLSL string
        internal static string ConvertType(Type t)
        {
            string tStr = "";

            if (t == typeof(KFloat))
            {
                tStr = "float";
            }
            else if (t == typeof(KInt)) tStr = "int";
            else if (t == typeof(Vec2)) tStr = "vec2";
            else if (t == typeof(Vec3)) tStr = "vec3";
            else if (t == typeof(Vec4)) tStr = "vec4";
            else if (t == typeof(Mat4)) tStr = "mat4";
            else if (t == typeof(Mat3)) tStr = "mat3";
            else if (t == typeof(Mat2)) tStr = "mat2";
            else if (t == typeof(Sampler1D)) tStr = "sampler1D";
            else if (t == typeof(Sampler2D)) tStr = "sampler2D";
            else if (t == typeof(Sampler3D)) tStr = "sampler3D";

            return tStr;
        }

        //Generate a type declaration
        internal static string TypeDeclaration(Type t, object val)
        {
            string tmp = ConvertType(t) + "(" + val.ToString() + ")";
            currentDeclaration = tmp;
            return tmp;
        }

        //Generate a return statement
        internal static void Return()
        {
            src += "return " + currentDeclaration;
        }
    }
}
