using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Reducers
{
    public class RoutingProductionTimeManagerNewPageSizeReducer : Reducer<RoutingProductionTimePageState, RoutingProductionTimeManagerPageSizeChangedAction>
    {
        public override RoutingProductionTimePageState Reduce(RoutingProductionTimePageState state, RoutingProductionTimeManagerPageSizeChangedAction action)
            => new RoutingProductionTimePageState(state.PageIndex,
                                                  action.NewPageSize,
                                                  state.IsLoading,
                                                  state.Errors,
                                                  state.Data);
    }
}
