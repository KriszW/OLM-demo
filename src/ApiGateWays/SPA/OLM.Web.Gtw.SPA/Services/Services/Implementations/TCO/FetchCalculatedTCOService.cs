using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.TCO
{
    public class FetchCalculatedTCOService : ITCOCalculatorService
    {
        private IHttpClientFactory _httpClientFactory;
        private ServiceUrlSettings _serviceUrls;

        public FetchCalculatedTCOService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<BundleTCOAPIResponseViewModel> CalculateAVGTCO(IEnumerable<string> bundleIDs)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.TCO.TCOAggregator.AggregateAVG(_serviceUrls.TCO);

            var jsonText = JsonSerializer.Serialize(bundleIDs);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            var result = await respone.Content.ReadFromJsonAsync<APIResponse<BundleTCOAPIResponseViewModel>>();

            return result.TryGetModel();
        }

        public async Task<BundleTCOAPIResponseViewModel> CalculateTCO(string bundleID)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.TCO.TCOAggregator.Aggregate(_serviceUrls.TCO, bundleID);
            var respone = await client.GetAsync(route);

            var result = await respone.Content.ReadFromJsonAsync<APIResponse<BundleTCOAPIResponseViewModel>>();

            return result.TryGetModel();
        }
    }
}
