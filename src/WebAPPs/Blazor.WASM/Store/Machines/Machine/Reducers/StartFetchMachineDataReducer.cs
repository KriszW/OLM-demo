using Fluxor;
using OLM.Blazor.WASM.Store.Machines.Machine.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Machines.Machine.Reducers
{
    public class StartFetchMachineDataReducer : Reducer<MachineState, StartFetchMachineDataAction>
    {
        public override MachineState Reduce(MachineState state,
                                            StartFetchMachineDataAction action)
            => new MachineState(true, default, action.MachineID);
    }
}
