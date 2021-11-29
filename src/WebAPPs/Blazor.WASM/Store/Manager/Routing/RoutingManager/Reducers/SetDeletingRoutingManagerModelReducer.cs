using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Reducers
{
    public class SetDeletingRoutingManagerModelReducer : Reducer<RoutingManagerPageState, RoutingManagerForDeletingSetAction>
    {
        public override RoutingManagerPageState Reduce(RoutingManagerPageState state, RoutingManagerForDeletingSetAction action)
            => new RoutingManagerPageState(state.PageIndex,
                                           state.PageSize,
                                           state.IsLoading,
                                           state.Errors,
                                           state.Data,
                                           state.SelectedModel,
                                           state.ModelForEdit,
                                           action.Model);
    }
}
