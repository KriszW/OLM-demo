using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Reducers
{
    public class FetchRoutingManagersFailedReducer : Reducer<RoutingManagerPageState, FetchRoutingManagersFailedAction>
    {
        public override RoutingManagerPageState Reduce(RoutingManagerPageState state, FetchRoutingManagersFailedAction action)
            => new RoutingManagerPageState(state.PageIndex,
                                           state.PageSize,
                                           false,
                                           action.Errors,
                                           default,
                                           default,
                                           default,
                                           default);
    }
}
