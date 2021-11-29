using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Actions;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Reducers
{
    public class FetchCurrenciesSuccessReducer : Reducer<CurrencyManagerState, FetchCurrenciesSuccessAction>
    {
        public override CurrencyManagerState Reduce(CurrencyManagerState state, FetchCurrenciesSuccessAction action)
            => new CurrencyManagerState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     default,
                                     action.Model,
                                     default,
                                     default,
                                     default);
    }
}
