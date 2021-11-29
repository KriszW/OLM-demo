using Fluxor;
using OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Reducers
{
    public class FetchWeeklyRoutingFailReducer : Reducer<WeeklyRoutingPageState, FetchWeeklyRoutingFailedAction>
    {
        public override WeeklyRoutingPageState Reduce(WeeklyRoutingPageState state, FetchWeeklyRoutingFailedAction action)
            => new WeeklyRoutingPageState(action.MachineName, action.ErrorMSG);
    }
}
