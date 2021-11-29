using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Reducers
{
    public class PauseNewPageIndexReducer : Reducer<PausesPageState, PausePageIndexChangedAction>
    {
        public override PausesPageState Reduce(PausesPageState state, PausePageIndexChangedAction action)
            => new PausesPageState(action.NewPageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
