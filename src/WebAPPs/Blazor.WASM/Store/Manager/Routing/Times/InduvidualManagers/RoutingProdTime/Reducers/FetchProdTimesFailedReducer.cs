using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Reducers
{
    public class FetchProdTimesFailedReducer : Reducer<ProdTimesPageState, FetchProdTimesFailedAction>
    {
        public override ProdTimesPageState Reduce(ProdTimesPageState state, FetchProdTimesFailedAction action)
            => new ProdTimesPageState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     action.Errors,
                                     default,
                                     default,
                                     default,
                                     default);
    }
}
