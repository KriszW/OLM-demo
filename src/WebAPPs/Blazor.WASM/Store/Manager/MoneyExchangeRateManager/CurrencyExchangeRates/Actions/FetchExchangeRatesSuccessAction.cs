using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using OLM.Shared.Models.Target.SharedAPIModels;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates.Actions
{
    public class FetchExchangeRatesSuccessAction
    {
        public FetchExchangeRatesSuccessAction(Paginated<ExchangeRateViewModel> model)
        {
            Model = model;
        }

        public Paginated<ExchangeRateViewModel> Model { get; private set; }
    }
}
