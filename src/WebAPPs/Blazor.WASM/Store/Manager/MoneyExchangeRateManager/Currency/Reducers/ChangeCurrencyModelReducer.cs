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
    public class ChangeCurrencyModelReducer : Reducer<CurrencyManagerState, ChangeCurrencyModelAction>
    {
        public override CurrencyManagerState Reduce(CurrencyManagerState state, ChangeCurrencyModelAction action)
            => new CurrencyManagerState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     action.NewModelForEdit,
                                     default);
    }
}
