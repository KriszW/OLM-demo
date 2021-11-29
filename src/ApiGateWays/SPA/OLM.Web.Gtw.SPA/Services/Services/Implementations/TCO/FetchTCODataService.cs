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
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.TCO
{
    public class FetchTCODataService : IFetchTCODataService
    {
        private IHttpClientFactory _httpClientFactory;
        private ServiceUrlSettings _serviceUrls;

        public FetchTCODataService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<IEnumerable<TCODataAPIResponseViewModel>> FetchModels(IEnumerable<string> bundleIDs)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.TCO.TCOController.GetTCOData(_serviceUrls.TCO);

            var jsonText = JsonSerializer.Serialize(bundleIDs);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            var result = await respone.Content.ReadFromJsonAsync<APIResponse<IEnumerable<TCODataAPIResponseViewModel>>>();

            return result.TryGetModel();
        }
    }
}
