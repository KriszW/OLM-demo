using Fluxor;
using OLM.Blazor.WASM.Store.Machines.Machine;
using OLM.Blazor.WASM.Store.Machines.MachinesSummarized.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Machines.MachinesSummarized.Reducers
{
    public class StartFetchSummarizedDateReducer : Reducer<MachineSummerizedState, StartFetchSummarizedDataAction>
    {
        public override MachineSummerizedState Reduce(MachineSummerizedState state, StartFetchSummarizedDataAction action)
            => new MachineSummerizedState();
    }
}
