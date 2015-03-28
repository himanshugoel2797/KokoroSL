using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL.Lib.Math
{
    public class Vec4 : Obj
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
                if (swizzleMask.Length == 4)
                {
                    return new Vec4()
                    {
                        ObjName = this.ObjName + "." + swizzleMask
                    };
                }
                else if (swizzleMask.Length == 3)
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

                return new Vec4()
                {
                    ObjName = this.ObjName
                };
            }
            set
            {
                if (swizzleMask.Length == 4)
                {
                    Manager.Assign<Vec4>(this.ObjName + "." + swizzleMask, value);
                }
                else if (swizzleMask.Length == 3)
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
        /// <returns>The defualt value</returns>
        public override object GetDefaultValue()
        {
            return 0;
        }

        #region Math Operators
        public static Vec4 operator *(Vec4 b, Mat4 a)
        {
            var k = new Vec4()
            {
                ObjName = "(" + a.ObjName + "*" + b.ObjName + ")"
            };

            SyntaxTree.Variables[k.ObjName] = new SyntaxTree.Variable()
            {
                type = k.GetType(),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            };

            return k;
        }

        public static Vec4 operator *(Mat4 a, Vec4 b)
        {
            var k = new Vec4()
            {
                ObjName = "(" + a.ObjName + "*" + b.ObjName + ")"
            };

            SyntaxTree.Variables[k.ObjName] = new SyntaxTree.Variable()
            {
                type = k.GetType(),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            };

            return k;
        }

        public static Vec4 operator /(Vec4 a, Mat4 b)
        {
            Vec4 k = new Vec4()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Mat4),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec4 operator *(KInt a, Vec4 b)
        {
            var k = new Vec4()
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

        public static Vec4 operator *(Vec4 a, Vec4 b)
        {
            var k = new Vec4()
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

        public static Vec4 operator /(Vec4 a, Vec4 b)
        {
            Vec4 k = new Vec4()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec4),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec4 operator /(KInt a, Vec4 b)
        {
            Vec4 k = new Vec4()
            {
                ObjName = "(" + a.ObjName + "/" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec4),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec4 operator +(Vec4 a, Vec4 b)
        {
            Vec4 k = new Vec4()
            {
                ObjName = "(" + a.ObjName + "+" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec4),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }

        public static Vec4 operator -(Vec4 a, Vec4 b)
        {
            Vec4 k = new Vec4()
            {
                ObjName = "(" + a.ObjName + "-" + b.ObjName + ")"
            };

            SyntaxTree.Variables.Add(k.ObjName, new SyntaxTree.Variable()
            {
                type = typeof(Vec4),
                value = null,
                paramType = SyntaxTree.ParameterType.Variable,
                name = k.ObjName
            });

            return k;
        }
        #endregion

        #region Logic Operators
        public static Vec4 operator <(Vec4 a, Vec4 b)
        {
            //Implement these properly, specifically, generate function calls for these (like GLSL builtins?) or manual comparison generation?
            return a;
        }
        public static Vec4 operator >(Vec4 a, Vec4 b)
        {
            return a;
        }
        #endregion

        #region Converters
        public static explicit operator Vec4(int i)
        {
            return new Vec4()
            {
                ObjName = i.ToString()
            };
        }

        public static implicit operator Vec4(KInt i)
        {
            return new Vec4()
            {
                ObjName = i.ObjName
            };
        }
        #endregion

        #region Non-Static Converters
        /// <summary>
        /// Construct a new Vec4 from a Vec3 object and a KFloat object
        /// </summary>
        /// <param name="vec">XYZ Component Vec3</param>
        /// <param name="i">W Component KFloat</param>
        public void Construct(Vec3 vec, KFloat i)
        {
            this["xyz"] = vec;
            this["w"] = i;
        }

        /// <summary>
        /// Construct a new Vec4 from 2 Vec2 objects
        /// </summary>
        /// <param name="xy">XY Component Vec2</param>
        /// <param name="zw">ZW Component Vec2</param>
        public void Construct(Vec2 xy, Vec2 zw)
        {
            this["xy"] = xy;
            this["zw"] = zw;
        }

        /// <summary>
        /// Construct a new Vec4 from 4 KFloat objects
        /// </summary>
        /// <param name="x">X Component KFloat</param>
        /// <param name="y">Y Component KFloat</param>
        /// <param name="z">Z Component KFloat</param>
        /// <param name="w">W Component KFloat</param>
        public void Construct(KFloat x, KFloat y, KFloat z, KFloat w)
        {
            this["x"] = x;
            this["y"] = y;
            this["z"] = z;
            this["w"] = w;
        }
        #endregion

    }
}
