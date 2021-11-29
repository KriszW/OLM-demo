using Fluxor;
using OLM.Blazor.WASM.Store.Routing.MachineRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Reducers
{
    public class StartFetchWeeklyRoutingReducer : Reducer<WeeklyRoutingPageState, StartFetchRoutingAction>
    {
        public override WeeklyRoutingPageState Reduce(WeeklyRoutingPageState state, StartFetchRoutingAction action)
            => new WeeklyRoutingPageState(action.MachineName);
    }
}
