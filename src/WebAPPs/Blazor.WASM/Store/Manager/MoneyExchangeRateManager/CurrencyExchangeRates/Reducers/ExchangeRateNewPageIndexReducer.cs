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
    public class ExchangeRateNewPageIndexReducer : Reducer<ExchangeRateManagerState, ExchangeRatePageIndexChangedAction>
    {
        public override ExchangeRateManagerState Reduce(ExchangeRateManagerState state, ExchangeRatePageIndexChangedAction action)
            => new ExchangeRateManagerState(state.ISOCode,
                                            action.NewPageIndex,
                                            state.PageSize,
                                            state.IsLoading,
                                            state.Errors,
                                            state.Data,
                                            default,
                                            default,
                                            default);
    }
}
