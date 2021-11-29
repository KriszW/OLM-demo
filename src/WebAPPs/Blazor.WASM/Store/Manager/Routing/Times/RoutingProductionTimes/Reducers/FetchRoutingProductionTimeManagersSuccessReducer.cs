using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Reducers
{
    public class FetchRoutingProductionTimeManagersSuccessReducer : Reducer<RoutingProductionTimePageState, FetchRoutingProductionTimeManagersSuccessAction>
    {
        public override RoutingProductionTimePageState Reduce(RoutingProductionTimePageState state, FetchRoutingProductionTimeManagersSuccessAction action)
            => new RoutingProductionTimePageState(state.PageIndex,
                                                  state.PageSize,
                                                  false,
                                                  default,
                                                  action.Model);
    }
}
