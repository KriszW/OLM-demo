using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Reducers
{
    public class RoutingProductionTimeManagerNewPageIndexReducer : Reducer<RoutingProductionTimePageState, RoutingProductionTimeManagerPageIndexChangedAction>
    {
        public override RoutingProductionTimePageState Reduce(RoutingProductionTimePageState state, RoutingProductionTimeManagerPageIndexChangedAction action)
            => new RoutingProductionTimePageState(action.NewPageIndex,
                                                  state.PageSize,
                                                  state.IsLoading,
                                                  state.Errors,
                                                  state.Data);
    }
}
