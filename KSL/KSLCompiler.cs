using Kokoro.KSL.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if GLSL
using CodeGenerator = Kokoro.KSL.GLSL.GLSLCodeGenerator;
#if PC
using Kokoro.KSL.GLSL.PC;
#endif
#endif

namespace Kokoro.KSL
{
    public class KSLCompiler
    {
        internal static Dictionary<string, Obj> preDefUniforms = new Dictionary<string, Obj>();

        public enum KShaderType
        {
            Vertex = 0, Fragment = 4, Geometry = 3, TessellationControl = 1, TessellationEval = 2, TessellationComb = 5
        }

        /// <summary>
        /// Register a predefined uniform name to be made available to shaders
        /// </summary>
        /// <typeparam name="T">The type of the uniform</typeparam>
        /// <param name="name">The name of the uniform</param>
        public static void RegisterPreDefinedUniform<T>(string name) where T : Obj, new()
        {
            preDefUniforms.Add(name, new T());
        }

        /// <summary>
        /// Unregister a predefined uniform
        /// </summary>
        /// <param name="name">The name of the uniform to unregister</param>
        public static void UnRegisterPreDefinedUniform(string name)
        {
            preDefUniforms.Remove(name);
        }

        /// <summary>
        /// Compile a shader program into its platform dependent equivalent
        /// </summary>
        /// <param name="shader">The shader to compile</param>
        /// <param name="s">The shader type to compile</param>
        /// <returns>The string representation of the shader in the language specified during build</returns>
        public static string Compile(IKShaderProgram shader, KShaderType s)
        {

            //Execute the object and collect the output code from the code generator

            switch (s)
            {
                case KShaderType.Vertex:
                    shader.Vertex();
                    break;
                case KShaderType.Fragment:
                    shader.Fragment();
                    break;
            }



            string vshader = CodeGenerator.CompileFromSyntaxTree(s);

            return vshader;
        }

    }
}
