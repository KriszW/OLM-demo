using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Reducers
{
    public class SelectedPauseModelChangedReducer : Reducer<PausesPageState, SelectedPauseModelChangedAction>
    {
        public override PausesPageState Reduce(PausesPageState state, SelectedPauseModelChangedAction action)
            => new PausesPageState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     action.NewSelectedModel,
                                     default,
                                     default);
    }
}
