using OLM.Services.Bundles.Prices.API.Extensions;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Services.Bundles.Prices.API.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System.Net.Http;
using System.Threading.Tasks;
using OLM.Services.MoneyExchangeRate.API.Models;

namespace OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceURLs _serviceURLs;

        public ExchangeRateRepository(ServiceURLs serviceURLs,
                                      IHttpClientFactory httpClientFactory)
        {
            _serviceURLs = serviceURLs;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CurrencyModel> GetRatesForCurrency(string sourceISOCode)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = _serviceURLs.ExchangeRates + $"api/exchange/{sourceISOCode}";

            var result = await client.GetWithJsonAsync<APIResponse<CurrencyModel>>(route);

            return result.TryGetModel();
        }
    }
}
