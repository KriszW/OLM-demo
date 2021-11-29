using Fluxor;
using OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Actions;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Reducers
{
    public class SetDeletingCurrencyModelReducer : Reducer<CurrencyManagerState, CurrencyModelForDeletingSetAction>
    {
        public override CurrencyManagerState Reduce(CurrencyManagerState state, CurrencyModelForDeletingSetAction action)
            => new CurrencyManagerState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     state.ModelForEdit,
                                     action.Model);
    }
}
