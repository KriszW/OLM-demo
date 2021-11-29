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
    public class CurrencyNewPageIndexReducer : Reducer<CurrencyManagerState, CurrencyPageIndexChangedAction>
    {
        public override CurrencyManagerState Reduce(CurrencyManagerState state, CurrencyPageIndexChangedAction action)
            => new CurrencyManagerState(action.NewPageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
