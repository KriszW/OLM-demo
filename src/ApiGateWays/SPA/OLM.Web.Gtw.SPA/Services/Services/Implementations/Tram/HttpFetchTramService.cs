using Microsoft.AspNetCore.WebUtilities;
using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Tram;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.Tram
{
    public class HttpFetchTramService : ITramService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public HttpFetchTramService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<IEnumerable<TramResponseViewModel>> Fetch(TramFetchRequestViewModel model)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var url = Endpoints.Tram.Trams.BuildFetch(_serviceUrls.Tram);

            var route = QueryHelpers.AddQueryString(url, 
                new Dictionary<string, string> 
                { 
                    { "Start", model.Start.ToString("s") }, 
                    { "End", model.End.ToString("s") } 
                });

            var result = await client.GetWithJsonAsync<APIResponse<IEnumerable<TramResponseViewModel>>>(route);

            return result.TryGetModel();
        }
    }
}
