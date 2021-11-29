using Fluxor;
using OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Reducers
{
    public class FetchWeeklyRoutingSuccessReducer : Reducer<WeeklyRoutingPageState, FetchWeeklyRoutingSucceededAction>
    {
        public override WeeklyRoutingPageState Reduce(WeeklyRoutingPageState state, FetchWeeklyRoutingSucceededAction action)
            => new WeeklyRoutingPageState(action.MachineName, action.Data);
    }
}
