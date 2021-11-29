using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Reducers
{
    public class RoutingPausesManagerNewPageSizeReducer : Reducer<RoutingPausesPageState, RoutingPausesManagerPageSizeChangedAction>
    {
        public override RoutingPausesPageState Reduce(RoutingPausesPageState state, RoutingPausesManagerPageSizeChangedAction action)
            => new RoutingPausesPageState(state.PageIndex,
                                          action.NewPageSize,
                                          state.IsLoading,
                                          state.Errors,
                                          state.Data);
    }
}
