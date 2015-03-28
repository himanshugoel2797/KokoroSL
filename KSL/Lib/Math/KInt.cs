using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL.Lib.Math
{
    /// <summary>
    /// Define a KSL Int object
    /// </summary>
    public class KInt : Obj
    {
        /// <summary>
        /// Get the default value of this object
        /// </summary>
        /// <returns>The default value</returns>
        public override object GetDefaultValue()
        {
            return 0;
        }

        #region Operators
        public static KInt operator *(KInt a, KInt b)
        {
            KInt k = new KInt()
            {
                ObjName = "(" + a.ObjName + "*" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(KInt),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static KInt operator /(KInt a, KInt b)
        {
            KInt k = new KInt()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(KInt),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static KInt operator +(KInt a, KInt b)
        {
            KInt k = new KInt()
            {
                ObjName = "(" + a.ObjName + "+" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(KInt),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static KInt operator -(KInt a, KInt b)
        {
            KInt k = new KInt()
            {
                ObjName = "(" + a.ObjName + "-" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(KInt),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }
        #endregion

        #region Converters
        public static implicit operator KInt(int i)
        {
            return new KInt() {
                ObjName = i.ToString()
            };
        }
        #endregion
    }
}
