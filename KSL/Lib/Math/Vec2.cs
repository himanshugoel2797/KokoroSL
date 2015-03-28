using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL.Lib.Math
{
    public class Vec2 : Obj
    {
        #region Indexer Hack

        /// <summary>
        /// Get/Set swizzle masks
        /// </summary>
        /// <param name="swizzleMask">The swizzle mask</param>
        /// <returns>The object with the swizzle mask applied</returns>
        public object this[string swizzleMask]
        {
            get
            {
                if (swizzleMask.Length == 2)
                {
                    return new Vec2()
                    {
                        ObjName = this.ObjName + "." + swizzleMask
                    };
                }
                else if (swizzleMask.Length == 1)
                {
                    return new KFloat()
                    {
                        ObjName = this.ObjName + "." + swizzleMask
                    };
                }

                return new Vec2()
                {
                    ObjName = this.ObjName
                };
            }
            set
            {
                if (swizzleMask.Length == 2)
                {
                    Manager.Assign<Vec2>(this.ObjName + "." + swizzleMask, value);
                }
                else if (swizzleMask.Length == 1)
                {
                    Manager.Assign<KFloat>(this.ObjName + "." + swizzleMask, value);
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
        public static Vec2 operator *(KInt a, Vec2 b)
        {
            var k = new Vec2()
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

        public static Vec2 operator *(Vec2 a, Vec2 b)
        {
            var k = new Vec2()
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

        public static Vec2 operator /(Vec2 a, Vec2 b)
        {
            Vec2 k = new Vec2()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec2),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec2 operator /(KInt a, Vec2 b)
        {
            Vec2 k = new Vec2()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec2),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            Vec2 k = new Vec2()
            {
                ObjName = "(" + a.ObjName + "+" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec2),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            Vec2 k = new Vec2()
            {
                ObjName = "(" + a.ObjName + "-" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec2),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }
        #endregion

        #region Converters
        public static explicit operator Vec2(int i)
        {
            return new Vec2()
            {
                ObjName = i.ToString()
            };
        }

        public static implicit operator Vec2(KInt i)
        {
            return new Vec2()
            {
                ObjName = i.ObjName
            };
        }
        #endregion

        #region Non-Static Converters
        /// <summary>
        /// Construct a Vec2 objecct from 2 KFloat objects
        /// </summary>
        /// <param name="a">X Component KFloat</param>
        /// <param name="b">Y Component KFloat</param>
        public void Construct(KFloat a, KFloat b)
        {
            this["x"] = a;
            this["y"] = b;
        }
        #endregion
    }
}
