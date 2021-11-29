using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Reducers
{
    public class PauseNewPageSizeReducer : Reducer<PausesPageState, PausePageSizeChangedAction>
    {
        public override PausesPageState Reduce(PausesPageState state, PausePageSizeChangedAction action)
            => new PausesPageState(state.PageIndex,
                                     action.NewPageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
