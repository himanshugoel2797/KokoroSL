using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.ComponentModel;
using Kokoro.KSL.Lib.General;

namespace Kokoro.KSL.Lib
{
    /// <summary>
    /// Manages variable creation
    /// </summary>
    public class Manager
    {
        internal static dynamic VarDB;

        private static void HandlePropertyChanges(
        object sender, PropertyChangedEventArgs e)
        {
            //Handle assignment operations so we can generate equivalent shading language instructions
            if (((IDictionary<string, object>)VarDB)[e.PropertyName] as Obj != null && e.PropertyName != ((Obj)((IDictionary<string, object>)VarDB)[e.PropertyName]).ObjName)
            {
                SyntaxTree.Instructions.Enqueue(new SyntaxTree.Instruction()
                {
                    instructionType = SyntaxTree.InstructionType.Assign,
                    Parameters = new string[] { e.PropertyName, ((Obj)((IDictionary<string, object>)VarDB)[e.PropertyName]).ObjName }
                });

                ((Obj)((IDictionary<string, object>)VarDB)[e.PropertyName]).ObjName = e.PropertyName;
            }
        }

        /// <summary>
        /// Marks the start of a new shader
        /// </summary>
        /// <returns>Provides a dynamic object which will contain all the shader's variables</returns>
        public static dynamic ShaderStart()
        {
            VarDB = new ExpandoObject();

            //Define predefined variables beforehand
            SyntaxTree.Variables = new Dictionary<string, SyntaxTree.Variable>();
            SyntaxTree.Variables.Add("VertexPosition", new SyntaxTree.Variable()
            {
                name = "VertexPosition",
                paramType = SyntaxTree.ParameterType.Variable,
                type = typeof(Math.Vec4),
                value = null
            });
            ((IDictionary<string, object>)VarDB).Add("VertexPosition", new Math.Vec4()
            {
                ObjName = "VertexPosition"
            });

            SyntaxTree.Variables.Add("VertexID", new SyntaxTree.Variable()
            {
                name = "VertexID",
                paramType = SyntaxTree.ParameterType.Variable,
                type = typeof(Math.KInt),
                value = null
            });
            ((IDictionary<string, object>)VarDB).Add("VertexID", new Math.KInt()
            {
                ObjName = "VertexID"
            });

            SyntaxTree.Variables.Add("InstanceID", new SyntaxTree.Variable()
            {
                name = "InstanceID",
                paramType = SyntaxTree.ParameterType.Variable,
                type = typeof(Math.KInt),
                value = null
            });
            ((IDictionary<string, object>)VarDB).Add("InstanceID", new Math.KInt()
            {
                ObjName = "InstanceID"
            });

            SyntaxTree.Variables.Add("FragCoord", new SyntaxTree.Variable()
            {
                name = "FragCoord",
                paramType = SyntaxTree.ParameterType.Variable,
                type = typeof(Math.Vec2),
                value = null
            });
            ((IDictionary<string, object>)VarDB).Add("FragCoord", new Math.Vec2()
            {
                ObjName = "FragCoord"
            });
            //TODO add more variables into the shader compiler?


            //Reset the engine state
            ((INotifyPropertyChanged)VarDB).PropertyChanged +=
            new PropertyChangedEventHandler(HandlePropertyChanges);

            SyntaxTree.AssignmentBuffer = new Queue<SyntaxTree.Instruction>();
            SyntaxTree.Instructions = new Queue<SyntaxTree.Instruction>();
            SyntaxTree.Parameters = new Dictionary<string, SyntaxTree.Variable>();
            SyntaxTree.SharedVariables = new Dictionary<string, SyntaxTree.Variable>();

            //PreDefine any variables requested by the host
            foreach (KeyValuePair<string, Obj> t in KSLCompiler.preDefUniforms)
            {
                var tmp = t.Value;
                tmp.ObjName = t.Key;

                SyntaxTree.Parameters.Add(t.Key, new SyntaxTree.Variable()
                {
                    type = tmp.GetType(),
                    value = null,
                    paramType = SyntaxTree.ParameterType.Uniform,
                    name = t.Key
                });

                SyntaxTree.Variables.Add(t.Key, SyntaxTree.Parameters[t.Key]);

                ((IDictionary<string, object>)VarDB).Add(t.Key, tmp);
            }

            return VarDB;
        }

        /// <summary>
        /// Marks the end of a shader object
        /// </summary>
        public static void ShaderEnd()
        {

        }

        /// <summary>
        /// Define a new shader shared input value
        /// </summary>
        /// <typeparam name="T">The type of the input</typeparam>
        /// <param name="name">The name of the variable</param>
        public static void SharedIn<T>(string name) where T : Obj, new()
        {
            T tmp = new T();
            tmp.ObjName = name;

            SyntaxTree.Parameters.Add(name, new SyntaxTree.Variable()
            {
                type = typeof(T),
                value = null,
                paramType = SyntaxTree.ParameterType.SharedIn,
                name = name
            });

            SyntaxTree.Variables.Add(name, SyntaxTree.Parameters[name]);

            ((IDictionary<string, object>)VarDB).Add(name, tmp);
        }

        /// <summary>
        /// Define a new shader shared output value
        /// </summary>
        /// <typeparam name="T">The type of the output</typeparam>
        /// <param name="name">The name of the variable</param>
        public static void SharedOut<T>(string name) where T : Obj, new()
        {
            T tmp = new T();
            tmp.ObjName = name;

            SyntaxTree.Parameters.Add(name, new SyntaxTree.Variable()
            {
                type = typeof(T),
                value = null,
                paramType = SyntaxTree.ParameterType.SharedOut,
                name = name
            });

            SyntaxTree.Variables.Add(name, SyntaxTree.Parameters[name]);

            ((IDictionary<string, object>)VarDB).Add(name, null);
        }

        /// <summary>
        /// Define a new Stream In location variable
        /// </summary>
        /// <typeparam name="T">The Type of the variable</typeparam>
        /// <param name="name">The name of the variable</param>
        /// <param name="location">The location of the variable</param>
        public static void StreamIn<T>(string name, int location) where T : Obj, new()
        {
            T tmp = new T();
            tmp.ObjName = name;

            SyntaxTree.Parameters.Add(name, new SyntaxTree.Variable()
            {
                type = typeof(T),
                value = location,
                paramType = SyntaxTree.ParameterType.StreamIn,
                name = name
            });

            SyntaxTree.Variables.Add(name, SyntaxTree.Parameters[name]);

            ((IDictionary<string, object>)VarDB).Add(name, tmp);
        }

        /// <summary>
        /// Define a new Stream Out location variable
        /// </summary>
        /// <typeparam name="T">The type of the variable</typeparam>
        /// <param name="name">The name of the variable</param>
        /// <param name="location">The location of the variable</param>
        public static void StreamOut<T>(string name, int location) where T : Obj, new()
        {
            T tmp = new T();
            tmp.ObjName = name;

            SyntaxTree.Parameters.Add(name, new SyntaxTree.Variable()
            {
                type = typeof(T),
                value = location,
                paramType = SyntaxTree.ParameterType.StreamOut,
                name = name
            });

            SyntaxTree.Variables.Add(name, SyntaxTree.Parameters[name]);

            ((IDictionary<string, object>)VarDB).Add(name, tmp);
        }

        /// <summary>
        /// Define a new uniform variable
        /// </summary>
        /// <typeparam name="T">The type of the variable</typeparam>
        /// <param name="name">The name of the variable</param>
        public static void Uniform<T>(string name) where T : Obj, new()
        {
            T tmp = new T();
            tmp.ObjName = name;

            SyntaxTree.Parameters.Add(name, new SyntaxTree.Variable()
            {
                type = typeof(T),
                value = null,
                paramType = SyntaxTree.ParameterType.Uniform,
                name = name
            });

            SyntaxTree.Variables.Add(name, SyntaxTree.Parameters[name]);

            ((IDictionary<string, object>)VarDB).Add(name, tmp);
        }

        /// <summary>
        /// Define a new variable
        /// </summary>
        /// <typeparam name="T">The type of the variable</typeparam>
        /// <param name="name">The name of the variable</param>
        public static void Create<T>(string name) where T : Obj, new()
        {
            T tmp = new T();
            tmp.ObjName = name;

            //TODO Implement type specific object value assigners
            SyntaxTree.Variables.Add(name, new SyntaxTree.Variable()
            {
                type = typeof(T),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = name
            });

            SyntaxTree.Instructions.Enqueue(new SyntaxTree.Instruction()
            {
                instructionType = SyntaxTree.InstructionType.Create,
                Parameters = new string[] { name }
            });

            ((IDictionary<string, object>)VarDB).Add(name, tmp);
        }

        static int tmpVarIDs = 0;
        internal static string TemporaryVariable<T>() where T : Obj, new()
        {
            Create<T>("tmp" + tmpVarIDs++);
            return "tmp" + (tmpVarIDs - 1);
        }

        internal static void Assign<T>(string name, object val)
        {
            if (!SyntaxTree.Variables.ContainsKey(name))
            {
                SyntaxTree.Variables.Add(name, new SyntaxTree.Variable());
            }

            SyntaxTree.Variables[name] = new SyntaxTree.Variable()
            {
                type = typeof(T),
                value = val,
                paramType = SyntaxTree.ParameterType.Variable,
                name = name
            };

            ((IDictionary<string, object>)VarDB)[name] = val;
        }

    }
}
