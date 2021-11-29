using OLM.Services.MoneyExchangeRate.API.Models;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions
{
    public interface IExchangeRateRepository
    {
        Task<CurrencyModel> GetRatesForCurrency(string sourceISOCode);
    }
}
