using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Machines.Machine.Actions
{
    public class StartFetchMachineDataAction
    {
        public StartFetchMachineDataAction(string machineID)
        {
            MachineID = machineID;
        }

        public string MachineID { get; private set; }
    }
}
