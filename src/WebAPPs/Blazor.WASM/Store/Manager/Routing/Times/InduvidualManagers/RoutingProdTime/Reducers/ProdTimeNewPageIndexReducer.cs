using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Reducers
{
    public class ProdTimeNewPageIndexReducer : Reducer<ProdTimesPageState, PausePageIndexChangedAction>
    {
        public override ProdTimesPageState Reduce(ProdTimesPageState state, PausePageIndexChangedAction action)
            => new ProdTimesPageState(action.NewPageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
