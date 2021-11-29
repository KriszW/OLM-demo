using Fluxor;
using OLM.Blazor.WASM.Store.Routing.MachineRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.MachineRouting.Reducers
{
    public class FetchRoutingFailReducer : Reducer<MachineRoutingState, FetchRoutingFailedAction>
    {
        public override MachineRoutingState Reduce(MachineRoutingState state, FetchRoutingFailedAction action)
            => new MachineRoutingState(action.MachineName, action.ErrorMSG);
    }
}
