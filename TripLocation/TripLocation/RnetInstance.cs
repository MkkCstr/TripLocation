using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;
using TripLocation.Model;

namespace TripLocation
{
    class RnetInstance
    {
        public void runScript(HashSet<TableInfo> mytable)
        {
            REngine.SetEnvironmentVariables(); // <-- May be omitted; the next line would call it.
            REngine engine = REngine.GetInstance();


            engine.Dispose();
        }
    }
}
