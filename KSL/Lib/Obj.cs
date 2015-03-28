using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL.Lib
{
    public class Obj
    {
        internal string ObjName;

        public virtual object GetDefaultValue()
        {
            return null;
        }
    }
}
