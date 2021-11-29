using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Reducers
{
    public class SetDeletingProdTimeModelReducer : Reducer<ProdTimesPageState, ProdTimeModelForDeletingSetAction>
    {
        public override ProdTimesPageState Reduce(ProdTimesPageState state, ProdTimeModelForDeletingSetAction action)
            => new ProdTimesPageState(state.PageIndex,
                                      state.PageSize,
                                      state.IsLoading,
                                      state.Errors,
                                      state.Data,
                                      state.SelectedModel,
                                      state.ModelForEdit,
                                      action.Model);
    }
}
