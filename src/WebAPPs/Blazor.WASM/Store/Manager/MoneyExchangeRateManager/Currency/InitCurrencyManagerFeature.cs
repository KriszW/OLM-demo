using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency
{
    public class InitCurrencyManagerFeature : Feature<CurrencyManagerState>
    {
        public override string GetName() => "CurrencyCRUD";

        protected override CurrencyManagerState GetInitialState()
            => new CurrencyManagerState(0, 25);
    }
}
