using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions
{
    public class LoadCurrenciesSuccessAction
    {
        public LoadCurrenciesSuccessAction(List<string> currencies)
        {
            Currencies = currencies;
        }

        public List<string> Currencies { get; set; }
    }
}
