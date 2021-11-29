using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Reducers
{
    public class RoutingPausesManagerNewPageIndexReducer : Reducer<RoutingPausesPageState, RoutingPausesManagerPageIndexChangedAction>
    {
        public override RoutingPausesPageState Reduce(RoutingPausesPageState state, RoutingPausesManagerPageIndexChangedAction action)
            => new RoutingPausesPageState(action.NewPageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data);
    }
}
