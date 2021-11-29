using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.MoneyExchange
{
    public interface ICurrencyExchangeService
    {
        Task<CurrencyExchangedDataViewModel> Exchange(ExchangeCurrencyViewModel model);
    }
}
