using Fluxor;
using OLM.Blazor.WASM.Store.Routing.DailyRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.DailyRouting.Reducers
{
    public class FetchDailyRoutingFailReducer : Reducer<DailyRoutingPageState, FetchDailyRoutingFailedAction>
    {
        public override DailyRoutingPageState Reduce(DailyRoutingPageState state, FetchDailyRoutingFailedAction action)
            => new DailyRoutingPageState(action.MachineName, action.ErrorMSG);
    }
}
