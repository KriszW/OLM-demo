using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.MachineRouting.Actions
{
    public class FetchRoutingFailedAction
    {
        public FetchRoutingFailedAction(string machineName, string errorMSG)
        {
            MachineName = machineName;
            ErrorMSG = errorMSG;
        }

        public string MachineName { get; set; }

        public string ErrorMSG { get; set; }
    }
}
