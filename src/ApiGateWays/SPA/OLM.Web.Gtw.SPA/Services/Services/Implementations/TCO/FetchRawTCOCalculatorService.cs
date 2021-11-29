using Microsoft.AspNetCore.WebUtilities;
using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpMessage.URIHelpers;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OLM.Services.SharedBases.APIErrors;
using OneOf;
using OLM.Shared.Extensions.OneOfExtensions;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.TCO
{
    public class FetchRawTCOCalculatorService : IFetchRawTCOCalculatorService
    {
        private IHttpClientFactory _httpClientFactory;
        private ServiceUrlSettings _serviceUrls;

        public FetchRawTCOCalculatorService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<OneOf<BundleTCOAPIResponseViewModel, APIError>> CalculateAVGTCO(IEnumerable<RawTCOQueryDataViewModel> model)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.TCO.RawTCOAggregator.AggregateAVG(_serviceUrls.TCO);

            var jsonText = JsonSerializer.Serialize(model);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            var result = await respone.Content.ReadFromJsonAsync<APIResponse<BundleTCOAPIResponseViewModel>>();

            return result.TryMatchModel();
        }

        public async Task<OneOf<BundleTCOAPIResponseViewModel, APIError>> CalculateTCO(RawTCOQueryDataViewModel model)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.TCO.RawTCOAggregator.Aggregate(_serviceUrls.TCO);
            var queryString = model.ToQueryString();
            var requestUri = route + "?" + queryString;
            var result = await client.GetWithJsonAsync<APIResponse<BundleTCOAPIResponseViewModel>>(requestUri);

            return result.TryMatchModel();
        }
    }
}
