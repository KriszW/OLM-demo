using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.MoneyExchange;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.MoneyExchange
{
    public class HttpCurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public HttpCurrencyExchangeService(IHttpClientFactory httpClientFactory,
                                           ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<CurrencyExchangedDataViewModel> Exchange(ExchangeCurrencyViewModel model)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.MoneyExchangeRate.Exchange.BuildExchange(_serviceUrls.MoneyExchangeRate);
            var requestUri = route + $"?SourceCurrency={model.SourceCurrency}&DestinationCurrency={model.DestinationCurrency}";
            var result = await client.GetWithJsonAsync<APIResponse<CurrencyExchangedDataViewModel>>(requestUri);

            return result.TryGetModel();
        }
    }
}
