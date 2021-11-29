using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager.Actions
{
    public class SetMachineIDAction
    {
        public SetMachineIDAction(string machineID)
        {
            MachineID = machineID;
        }

        public string MachineID { get; set; }
    }
}
