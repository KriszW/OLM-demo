using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Reducers
{
    public class RoutingManagerNewPageIndexReducer : Reducer<RoutingManagerPageState, RoutingManagerPageIndexChangedAction>
    {
        public override RoutingManagerPageState Reduce(RoutingManagerPageState state, RoutingManagerPageIndexChangedAction action)
            => new RoutingManagerPageState(action.NewPageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
