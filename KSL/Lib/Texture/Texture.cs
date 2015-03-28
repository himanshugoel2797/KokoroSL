using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kokoro.KSL.Lib.Math;

#if GLSL
using CodeGenerator = Kokoro.KSL.GLSL.GLSLCodeGenerator;
#if PC
using Kokoro.KSL.GLSL.PC;
#endif
#endif

namespace Kokoro.KSL.Lib.Texture
{
    public static class Texture
    {
        /// <summary>
        /// Sample a 2D texture for its color values 
        /// </summary>
        /// <param name="sampler">The 2D texture to sample from</param>
        /// <param name="uv">The UV coordinates from which to sample</param>
        /// <returns>Returns the sampled Color</returns>
        public static Vec4 Read2D(Sampler2D sampler, Vec2 uv)
        {
            var k = new Vec4()
            {
                ObjName = CodeGenerator.TranslateSDKFunctionCalls( SyntaxTree.FunctionCalls.Tex2D, sampler.ObjName, uv.ObjName)
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

        /// <summary>
        /// Sample a 1D texture for its color values 
        /// </summary>
        /// <param name="sampler">The 1D texture to sample from</param>
        /// <param name="uv">The UV coordinates from which to sample</param>
        /// <returns>Returns the sampled Color</returns>
        public static Vec4 Read1D(Sampler1D sampler, KFloat uv)
        {
            var k = new Vec4()
            {
                ObjName = CodeGenerator.TranslateSDKFunctionCalls(SyntaxTree.FunctionCalls.Tex1D, sampler.ObjName, uv.ObjName)
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

        /// <summary>
        /// Sample a 3D texture for its color values 
        /// </summary>
        /// <param name="sampler">The 3D texture to sample from</param>
        /// <param name="uv">The UV coordinates from which to sample</param>
        /// <returns>Returns the sampled Color</returns>
        public static Vec4 Read3D(Sampler3D sampler, Vec3 uv)
        {
            var k = new Vec4()
            {
                ObjName = CodeGenerator.TranslateSDKFunctionCalls(SyntaxTree.FunctionCalls.Tex3D, sampler.ObjName, uv.ObjName)
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

        //TODO implement gather functions etc
    }
}
