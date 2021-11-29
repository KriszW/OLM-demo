using OLM.Services.Routing.API.Extensions;
using OLM.Services.Routing.API.Services.Repositories.Abstractions;
using OLM.Services.Routing.API.Utilities.Settings;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Response;
using OLM.Shared.Extensions.HttpMessage.URIHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;

namespace OLM.Services.Routing.API.Services.Repositories.Implementations
{
    public class RoutingDataRepository : IRoutingDataRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServicesSettings _servicesSettings;

        public RoutingDataRepository(IHttpClientFactory httpClientFactory,
                                     ServicesSettings servicesSettings)
        {
            _httpClientFactory = httpClientFactory;
            _servicesSettings = servicesSettings;
        }

        public async Task<RoutingDataResponseViewModel> Fetch(DateTime start, DateTime end, string machineName)
        {
            var client = _httpClientFactory.CreateClientForServices();

            var model = new FetchRoutingDataRequestViewModel
            {
                Start = start,
                End = end,
                MachineName = machineName
            };

            var url = _servicesSettings.RoutingData + $"api/routingdata/calculate";
            var route = $"{url}?Start={start:s}&End={end:s}&MachineName={machineName}";

            var result = await client.GetWithJsonAsync<APIResponse<RoutingDataResponseViewModel>>(route);

            return result.TryGetModel();
        }
    }
}
