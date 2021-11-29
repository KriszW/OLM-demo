using Microsoft.EntityFrameworkCore;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions;
using OLM.Services.MoneyExchangeRate.API.Services.Services.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Services.Implementations
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        public static decimal ErrorOutputRate => -1;

        private readonly IExchangeRepository _exchangeRatesRepository;
        private readonly MoneyExchangeRatesDbContext _moneyExchangeRatesDbContext;

        public CurrencyExchangeService(IExchangeRepository exchangeRatesRepository,
                                       MoneyExchangeRatesDbContext moneyExchangeRatesDbContext)
        {
            _exchangeRatesRepository = exchangeRatesRepository;
            _moneyExchangeRatesDbContext = moneyExchangeRatesDbContext;
        }

        public async Task<decimal> Exchange(ExchangeCurrencyViewModel model)
        {
            var rate = await _exchangeRatesRepository.GetRateByISOCodes(model.SourceCurrency, model.DestinationCurrency);

            return rate != default ? rate.Rate : ErrorOutputRate;
        }

        public Task<CurrencyModel> GetByISOCode(string code) => _moneyExchangeRatesDbContext.Currencies.Include(m => m.Rates).FirstOrDefaultAsync(m => m.ISOCode == code);
    }
}
