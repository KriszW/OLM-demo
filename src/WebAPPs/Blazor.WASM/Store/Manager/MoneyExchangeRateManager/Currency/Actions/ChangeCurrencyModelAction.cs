using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using OLM.Shared.Models.Target.SharedAPIModels;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Actions
{
    public class ChangeCurrencyModelAction
    {
        public ChangeCurrencyModelAction(CurrencyViewModel newModelForEdit)
        {
            NewModelForEdit = newModelForEdit;
        }

        public CurrencyViewModel NewModelForEdit { get; set; }
    }
}
