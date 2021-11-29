using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Services.Abstractions
{
    public interface ICurrencyExchangeService
    {
        Task<CurrencyModel> GetByISOCode(string code);

        Task<decimal> Exchange(ExchangeCurrencyViewModel model);
    }
}
