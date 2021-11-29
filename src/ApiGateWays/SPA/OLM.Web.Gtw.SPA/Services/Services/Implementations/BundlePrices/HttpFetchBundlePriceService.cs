using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.BundlePrices;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OLM.Services.SharedBases.APIErrors;
using OneOf;
using OLM.Shared.Extensions.OneOfExtensions;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.BundlePrices
{
    public class HttpFetchBundlePriceService : IFetchBundlePriceService
    {
        private IHttpClientFactory _httpClientFactory;
        private ServiceUrlSettings _serviceUrls;

        public HttpFetchBundlePriceService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<OneOf<RawTCOQueryDataViewModel, APIError>> Fetch(string bundleID)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.BundlePrices.BundleIDPrice.GetForOne(_serviceUrls.BundlePrices, bundleID);
            var result = await client.GetWithJsonAsync<APIResponse<RawTCOQueryDataViewModel>>(route);

            return result.TryMatchModel();
        }

        public async Task<OneOf<IEnumerable<RawTCOQueryDataViewModel>, APIError>> Fetch(IEnumerable<string> bundleID)
        {
            var client = _httpClientFactory.CreateClientForServices();

            var route = Endpoints.BundlePrices.BundleIDPrice.GetForMany(_serviceUrls.BundlePrices);

            var jsonText = JsonSerializer.Serialize(bundleID);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            var result = await respone.Content.ReadFromJsonAsync<APIResponse<IEnumerable<RawTCOQueryDataViewModel>>>();

            return result.TryMatchModel();
        }
    }
}
