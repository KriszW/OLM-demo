using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Reducers
{
    public class ChangeProdTimeModelReducer : Reducer<ProdTimesPageState, ChangeProdTimeModelAction>
    {
        public override ProdTimesPageState Reduce(ProdTimesPageState state, ChangeProdTimeModelAction action)
            => new ProdTimesPageState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     action.NewModelForEdit,
                                     default);
    }
}
