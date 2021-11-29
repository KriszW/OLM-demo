using Microsoft.EntityFrameworkCore;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly MoneyExchangeRatesDbContext _dbContext;

        public ExchangeRepository(MoneyExchangeRatesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ExchangeRateModel> GetRateByISOCodes(string sourceISOCode, string destISOCode)
        {
            var currency = await _dbContext.Currencies.Include(c => c.Rates).FirstOrDefaultAsync(m => m.ISOCode == sourceISOCode)
                           ?? throw new APIErrorException($"A {sourceISOCode} valuta nincs feltöltve az adatbázisba");

            return currency.Rates.FirstOrDefault(m => m.DestISOCode == destISOCode)
                   ?? throw new APIErrorException($"A {sourceISOCode} valutához nincsen {destISOCode} valuta feltöltve átváltási rátával");
        }
    }
}
