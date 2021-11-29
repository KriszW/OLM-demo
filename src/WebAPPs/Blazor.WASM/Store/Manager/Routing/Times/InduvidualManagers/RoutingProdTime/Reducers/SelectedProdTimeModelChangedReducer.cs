using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Reducers
{
    public class SelectedProdTimeModelChangedReducer : Reducer<ProdTimesPageState, SelectedProdTimeModelChangedAction>
    {
        public override ProdTimesPageState Reduce(ProdTimesPageState state, SelectedProdTimeModelChangedAction action)
            => new ProdTimesPageState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     action.NewSelectedModel,
                                     default,
                                     default);
    }
}
