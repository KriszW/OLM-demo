using OLM.Services.MoneyExchangeRate.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions
{
    public interface IExchangeRepository
    {
        Task<ExchangeRateModel> GetRateByISOCodes(string sourceISOCode, string destISOCode);
    }
}
