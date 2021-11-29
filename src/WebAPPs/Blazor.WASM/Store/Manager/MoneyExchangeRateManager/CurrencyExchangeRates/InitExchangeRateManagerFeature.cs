using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates
{
    public class InitExchangeRateManagerFeature : Feature<ExchangeRateManagerState>
    {
        public override string GetName() => "ExchangeRateCRUD";

        protected override ExchangeRateManagerState GetInitialState()
            => new ExchangeRateManagerState(default, 0, 25);
    }
}
