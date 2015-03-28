using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL.Lib.Math
{
    public class Vec3 : Obj
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
                if (swizzleMask.Length == 3)
                {
                    return new Vec3()
                    {
                        ObjName = this.ObjName + "." + swizzleMask
                    };
                }
                else if (swizzleMask.Length == 2)
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

                return new Vec3()
                {
                    ObjName = this.ObjName
                };
            }
            set
            {
                if (swizzleMask.Length == 3)
                {
                    Manager.Assign<Vec3>(this.ObjName + "." + swizzleMask, value);
                }
                else if (swizzleMask.Length == 2)
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
        public static Vec3 operator *(Vec3 b, KFloat a) { return a * b; }
        public static Vec3 operator *(KFloat a, Vec3 b)
        {
            var k = new Vec3()
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

        public static Vec3 operator *(Vec3 a, Vec3 b)
        {
            var k = new Vec3()
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

        public static Vec3 operator /(Vec3 a, Vec3 b)
        {
            Vec3 k = new Vec3()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec3),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec3 operator /(KFloat a, Vec3 b)
        {
            Vec3 k = new Vec3()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec3),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            Vec3 k = new Vec3()
            {
                ObjName = "(" + a.ObjName + "+" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec3),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            Vec3 k = new Vec3()
            {
                ObjName = "(" + a.ObjName + "-" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec3),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }
        #endregion

        #region Converters
        public static explicit operator Vec3(int i)
        {
            return new Vec3()
            {
                ObjName = i.ToString()
            };
        }

        public static implicit operator Vec3(KFloat i)
        {
            return new Vec3()
            {
                ObjName = i.ObjName
            };
        }
        #endregion

        #region Non-Static Converters
        /// <summary>
        /// Construct a Vec3 object from a Vec2 object and a KFloat object
        /// </summary>
        /// <param name="vec">XY Component Vec2</param>
        /// <param name="i">Z Component KFloat</param>
        public void Construct(Vec2 vec, KFloat i)
        {
            this["xy"] = vec;
            this["z"] = i;
        }

        /// <summary>
        /// Construct a Vec3 object from 3 KFloat objects
        /// </summary>
        /// <param name="x">X Component KFloat</param>
        /// <param name="y">Y Component KFloat</param>
        /// <param name="z">Z Component KFloat</param>
        public void Construct(KFloat x, KFloat y, KFloat z)
        {
            this["x"] = x;
            this["y"] = y;
            this["z"] = z;
        }
        #endregion
    }
}
