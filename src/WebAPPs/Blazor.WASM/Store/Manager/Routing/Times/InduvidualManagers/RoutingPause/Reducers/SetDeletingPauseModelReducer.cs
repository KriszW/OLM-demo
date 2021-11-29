using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Reducers
{
    public class SetDeletingPauseModelReducer : Reducer<PausesPageState, PauseModelForDeletingSetAction>
    {
        public override PausesPageState Reduce(PausesPageState state, PauseModelForDeletingSetAction action)
            => new PausesPageState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     state.ModelForEdit,
                                     action.Model);
    }
}
