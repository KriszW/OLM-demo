using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates.Actions;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates.Reducers
{
    public class FetchExchangeRatesSuccessReducer : Reducer<ExchangeRateManagerState, FetchExchangeRatesSuccessAction>
    {
        public override ExchangeRateManagerState Reduce(ExchangeRateManagerState state, FetchExchangeRatesSuccessAction action)
            => new ExchangeRateManagerState(state.ISOCode,
                                            state.PageIndex,
                                            state.PageSize,
                                            false,
                                            default,
                                            action.Model,
                                            default,
                                            default,
                                            default);
    }
}
