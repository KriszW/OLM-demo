using Fluxor;
using OLM.Blazor.WASM.Store.Routing.MachineRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.MachineRouting.Reducers
{
    public class StartFetchingReducer : Reducer<MachineRoutingState, StartFetchRoutingAction>
    {
        public override MachineRoutingState Reduce(MachineRoutingState state, StartFetchRoutingAction action)
            => new MachineRoutingState(action.MachineName);
    }
}
