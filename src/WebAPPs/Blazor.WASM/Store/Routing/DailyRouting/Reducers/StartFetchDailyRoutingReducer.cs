using Fluxor;
using OLM.Blazor.WASM.Store.Routing.DailyRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.DailyRouting.Reducers
{
    public class StartFetchDailyRoutingReducer : Reducer<DailyRoutingPageState, StartFetchingDailyRoutingAction>
    {
        public override DailyRoutingPageState Reduce(DailyRoutingPageState state, StartFetchingDailyRoutingAction action)
            => new DailyRoutingPageState(action.MachineName);
    }
}
