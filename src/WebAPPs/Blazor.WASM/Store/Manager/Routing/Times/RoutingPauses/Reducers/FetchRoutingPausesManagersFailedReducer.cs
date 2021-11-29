using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Reducers
{
    public class FetchRoutingPausesManagersFailedReducer : Reducer<RoutingPausesPageState, FetchRoutingPausesManagersFailedAction>
    {
        public override RoutingPausesPageState Reduce(RoutingPausesPageState state, FetchRoutingPausesManagersFailedAction action)
            => new RoutingPausesPageState(state.PageIndex,
                                           state.PageSize,
                                           false,
                                           action.Errors,
                                           default);
    }
}
