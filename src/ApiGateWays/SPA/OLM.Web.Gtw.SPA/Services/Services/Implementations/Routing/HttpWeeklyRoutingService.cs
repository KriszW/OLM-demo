using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Routing;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.Routing
{
    public class HttpWeeklyRoutingService : IWeeklyRoutingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public HttpWeeklyRoutingService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }


        public async Task<RoutingResponseViewModel> FetchForWeek(string machineID)
        {
            var start = DateTime.Now.GetDateOfTheStartOfWeek(DayOfWeek.Monday);
            var end = DateTime.Now.Date.GetDateOfTheEndOfWeek(DayOfWeek.Sunday);

            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.Routing.GetRoutingCalculater(_serviceUrls.Routing);
            var requestUri = route + $"?Start={start:s}&End={end:s}&MachineName={machineID}";
            var result = await client.GetWithJsonAsync<APIResponse<RoutingResponseViewModel>>(requestUri);

            return result.TryGetModel();
        }
    }
}
