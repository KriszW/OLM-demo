using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Reducers
{
    public class SelectedRoutingManagerModelChangedReducer : Reducer<RoutingManagerPageState, SelectedRoutingManagerModelChangedAction>
    {
        public override RoutingManagerPageState Reduce(RoutingManagerPageState state, SelectedRoutingManagerModelChangedAction action)
            => new RoutingManagerPageState(state.PageIndex,
                                           state.PageSize,
                                           state.IsLoading,
                                           state.Errors,
                                           state.Data,
                                           action.NewSelectedModel,
                                           default,
                                           default);
    }
}
