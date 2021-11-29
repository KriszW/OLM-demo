using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Reducers
{
    public class FetchPausesFailedReducer : Reducer<PausesPageState, FetchPausesFailedAction>
    {
        public override PausesPageState Reduce(PausesPageState state, FetchPausesFailedAction action)
            => new PausesPageState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     action.Errors,
                                     default,
                                     default,
                                     default,
                                     default);
    }
}
