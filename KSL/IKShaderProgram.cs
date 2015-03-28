using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL
{
    public interface IKShaderProgram
    {
        void Fragment();
        void Vertex();
    }
}
