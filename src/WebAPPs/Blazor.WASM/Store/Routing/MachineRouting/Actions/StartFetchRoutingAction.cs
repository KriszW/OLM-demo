using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.MachineRouting.Actions
{
    public class StartFetchRoutingAction
    {
        public StartFetchRoutingAction(string machineName)
        {
            MachineName = machineName;
        }

        public string MachineName { get; set; }
    }
}
