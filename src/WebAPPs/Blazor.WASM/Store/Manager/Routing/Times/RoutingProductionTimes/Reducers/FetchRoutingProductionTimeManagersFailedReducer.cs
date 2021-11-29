using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Reducers
{
    public class FetchRoutingProductionTimeManagersFailedReducer : Reducer<RoutingProductionTimePageState, FetchRoutingProductionTimeManagersFailedAction>
    {
        public override RoutingProductionTimePageState Reduce(RoutingProductionTimePageState state, FetchRoutingProductionTimeManagersFailedAction action)
            => new RoutingProductionTimePageState(state.PageIndex,
                                                  state.PageSize,
                                                  false,
                                                  action.Errors,
                                                  default);
    }
}
