using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Reducers
{
    public class RoutingManagerNewPageSizeReducer : Reducer<RoutingManagerPageState, RoutingManagerPageSizeChangedAction>
    {
        public override RoutingManagerPageState Reduce(RoutingManagerPageState state, RoutingManagerPageSizeChangedAction action)
            => new RoutingManagerPageState(state.PageIndex,
                                           action.NewPageSize,
                                           state.IsLoading,
                                           state.Errors,
                                           state.Data,
                                           default,
                                           default,
                                           default);
    }
}
