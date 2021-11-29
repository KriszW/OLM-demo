using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Reducers
{
    public class ChangeRoutingManagerModelReducer : Reducer<RoutingManagerPageState, ChangeRoutingManagerModelAction>
    {
        public override RoutingManagerPageState Reduce(RoutingManagerPageState state, ChangeRoutingManagerModelAction action)
            => new RoutingManagerPageState(state.PageIndex,
                                           state.PageSize,
                                           state.IsLoading,
                                           state.Errors,
                                           state.Data,
                                           state.SelectedModel,
                                           action.NewModelForEdit,
                                           default);
    }
}
