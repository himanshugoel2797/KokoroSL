using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL
{
    internal class SyntaxTree
    {
        internal struct Variable
        {
            public Type type;
            public object value;
            public ParameterType paramType;
            public string name;
        }

        internal enum ParameterType
        {
            StreamIn, StreamOut, Uniform, Variable, SharedIn, SharedOut, Constant
        }

        internal enum InstructionType
        {
            Assign, Math, Create
        }

        internal enum FunctionCalls
        {
            Tex1D, Tex2D, Tex3D,
            Cross2D, Cross3D, Cross4D,
            Normalize2D, Normalize3D, Normalize4D,
            Mod, Clamp
        }

        internal struct Instruction
        {
            public InstructionType instructionType;
            public string[] Parameters;
        }

        internal static Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();
        internal static Dictionary<string, Variable> Parameters = new Dictionary<string, Variable>();
        internal static Dictionary<string, Variable> SharedVariables = new Dictionary<string, Variable>();
        internal static Queue<Instruction> Instructions = new Queue<Instruction>();
        internal static Queue<Instruction> AssignmentBuffer = new Queue<Instruction>();
        internal static Dictionary<string, Variable> PreDefinedVariables = new Dictionary<string, Variable>();
    }
}
