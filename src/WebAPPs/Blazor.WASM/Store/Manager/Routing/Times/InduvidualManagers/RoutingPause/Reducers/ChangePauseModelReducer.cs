using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Reducers
{
    public class ChangePauseModelReducer : Reducer<PausesPageState, ChangePauseModelAction>
    {
        public override PausesPageState Reduce(PausesPageState state, ChangePauseModelAction action)
            => new PausesPageState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     action.NewModelForEdit,
                                     default);
    }
}
