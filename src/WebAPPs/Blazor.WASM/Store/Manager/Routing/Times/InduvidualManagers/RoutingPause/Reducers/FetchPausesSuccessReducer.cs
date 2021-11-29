using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Reducers
{
    public class FetchPausesSuccessReducer : Reducer<PausesPageState, FetchPausesSuccessAction>
    {
        public override PausesPageState Reduce(PausesPageState state, FetchPausesSuccessAction action)
            => new PausesPageState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     default,
                                     action.Model,
                                     default,
                                     default,
                                     default);
    }
}
