using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Reducers
{
    public class FetchProdTimesSuccessReducer : Reducer<ProdTimesPageState, FetchProdTimesSuccessAction>
    {
        public override ProdTimesPageState Reduce(ProdTimesPageState state, FetchProdTimesSuccessAction action)
            => new ProdTimesPageState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     default,
                                     action.Model,
                                     default,
                                     default,
                                     default);
    }
}
