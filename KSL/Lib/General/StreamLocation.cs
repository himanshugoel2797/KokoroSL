using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokoro.KSL.Lib.General
{
    //This is used to allow the engine to deal with StreamLocations, by creating an object type with the necessary metadata
    class StreamLocation : Obj
    {
        int sID;
        public StreamLocation(int stream)
        {
            sID = stream;
        }
    }
}
