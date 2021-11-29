using OLM.Services.Routing.API.Extensions;
using OLM.Services.Routing.API.Services.Repositories.Abstractions;
using OLM.Services.Routing.API.Utilities.Settings;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using OLM.Services.SharedBases.Responses;
using System.Net.Http.Json;

namespace OLM.Services.Routing.API.Services.Repositories.Implementations
{
    public class RoutingTimeRepository : IRoutingTimeRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServicesSettings _servicesSettings;

        public RoutingTimeRepository(IHttpClientFactory httpClientFactory,
                                     ServicesSettings servicesSettings)
        {
            _httpClientFactory = httpClientFactory;
            _servicesSettings = servicesSettings;
        }

        public async Task<RoutingTimesResponseViewModel> Fetch(DateTime start, DateTime end, string machineName)
        {
            var client = _httpClientFactory.CreateClientForServices();

            var url = _servicesSettings.RoutingTime + $"api/routingtime/calculate";
            var route = $"{url}?Start={start:s}&End={end:s}&MachineName={machineName}";

            var result = await client.GetWithJsonAsync<APIResponse<RoutingTimesResponseViewModel>>(route);

            return result.TryGetModel();
        }
    }
}
