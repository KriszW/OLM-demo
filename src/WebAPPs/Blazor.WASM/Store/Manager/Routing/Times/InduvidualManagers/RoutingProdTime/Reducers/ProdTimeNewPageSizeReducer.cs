using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Reducers
{
    public class ProdTimeNewPageSizeReducer : Reducer<ProdTimesPageState, ProdTimePageSizeChangedAction>
    {
        public override ProdTimesPageState Reduce(ProdTimesPageState state, ProdTimePageSizeChangedAction action)
            => new ProdTimesPageState(state.PageIndex,
                                     action.NewPageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
