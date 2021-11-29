using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using OLM.Shared.Models.Target.SharedAPIModels;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Actions
{
    public class FetchCurrenciesSuccessAction
    {
        public FetchCurrenciesSuccessAction(Paginated<CurrencyViewModel> model)
        {
            Model = model;
        }

        public Paginated<CurrencyViewModel> Model { get; private set; }
    }
}
