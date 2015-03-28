using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL.Lib.Math
{
    public class Mat3 : Obj
    {
        #region Indexer Hack
        /// <summary>
        /// Get/Set swizzle masks
        /// </summary>
        /// <param name="indiceA">The swizzle mask</param>
        /// <returns>The object with the swizzle mask applied</returns>
        public object this[int indiceA]
        {
            get
            {
                if (indiceA < 3)
                {
                    return new Vec3()
                    {
                        ObjName = this.ObjName + "[" + indiceA + "]"
                    };
                }

                throw new IndexOutOfRangeException();
            }
            set
            {
                if (indiceA < 3)
                {
                    Manager.Assign<Vec3>(this.ObjName + "[" + indiceA + "]", value);
                }
            }
        }
        #endregion

        /// <summary>
        /// Get the default value of this object
        /// </summary>
        /// <returns>The default value</returns>
        public override object GetDefaultValue()
        {
            return 0;
        }

        #region Operators
        public static Mat3 operator *(Vec3 a, Mat3 b)
        {
            var k = new Mat3()
            {
                ObjName = "(" + a.ObjName + "*" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = k.GetType(),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Mat3 operator *(Mat3 a, Mat3 b)
        {
            var k = new Mat3()
            {
                ObjName = "(" + a.ObjName + "*" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = k.GetType(),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Mat3 operator /(Mat3 a, Mat3 b)
        {
            Mat3 k = new Mat3()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Mat3),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Mat3 operator /(Vec3 a, Mat3 b)
        {
            Mat3 k = new Mat3()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Mat3),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Mat3 operator +(Mat3 a, Mat3 b)
        {
            Mat3 k = new Mat3()
            {
                ObjName = "(" + a.ObjName + "+" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Mat3),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Mat3 operator -(Mat3 a, Mat3 b)
        {
            Mat3 k = new Mat3()
            {
                ObjName = "(" + a.ObjName + "-" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Mat3),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }
        #endregion

        #region Converters
        //TODO Implement converters to Matrix3?
        #endregion

        #region Non-Static Converters
        /// <summary>
        /// Construct a Mat3 object from 3 Vec3 objects
        /// </summary>
        /// <param name="row0">Row 0 Vec3</param>
        /// <param name="row1">Row 1 Vec3</param>
        /// <param name="row2">Row 2 VEc3</param>
        public void Construct(Vec3 row0, Vec3 row1, Vec3 row2)
        {
            this[0] = row0;
            this[1] = row1;
            this[2] = row2;
        }
        #endregion
    }
}