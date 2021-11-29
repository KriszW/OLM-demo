using Fluxor;
using OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates.Actions;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates.Reducers
{
    public class FetchExchangeRatesFailedReducer : Reducer<ExchangeRateManagerState, FetchExchangeRatesFailedAction>
    {
        public override ExchangeRateManagerState Reduce(ExchangeRateManagerState state, FetchExchangeRatesFailedAction action)
            => new ExchangeRateManagerState(state.ISOCode,
                                            state.PageIndex,
                                            state.PageSize,
                                            false,
                                            action.Errors,
                                            default,
                                            default,
                                            default,
                                            default);
    }
}
