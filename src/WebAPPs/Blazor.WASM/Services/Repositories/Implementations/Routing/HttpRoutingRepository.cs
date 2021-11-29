using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Routing
{
    public class HttpRoutingRepository : IRoutingRepository
    {
        private readonly HttpClient _httpClient;

        public HttpRoutingRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<APIResponse<AggregatedRoutingViewModel>> Fetch(string machineID)
        {
            var route = APIGTWEndpoints.Routing.Calculate(machineID);

            try
            {
                return await _httpClient.GetWithJsonAsync<APIResponse<AggregatedRoutingViewModel>>(route);
            }
            catch (Exception)
            {
                return new APIResponse<AggregatedRoutingViewModel>($"'{machineID}' nevű szabászsorhoz a routing lekérdezés közben váratlan hiba lépett fel");
            }
        }

        public async Task<APIResponse<RoutingResponseViewModel>> FetchForDay(string machineID)
        {
            var route = APIGTWEndpoints.Routing.CalculateForDay(machineID);

            try
            {
                return await _httpClient.GetWithJsonAsync<APIResponse<RoutingResponseViewModel>>(route);
            }
            catch (Exception)
            {
                return new APIResponse<RoutingResponseViewModel>($"'{machineID}' nevű szabászsorhoz a napi routing lekérdezés közben váratlan hiba lépett fel");
            }
        }

        public async Task<APIResponse<RoutingResponseViewModel>> FetchForWeek(string machineID)
        {
            var route = APIGTWEndpoints.Routing.CalculateForWeek(machineID);

            try
            {
                return await _httpClient.GetWithJsonAsync<APIResponse<RoutingResponseViewModel>>(route);
            }
            catch (Exception)
            {
                return new APIResponse<RoutingResponseViewModel>($"'{machineID}' nevű szabászsorhoz a heti routing lekérdezés közben váratlan hiba lépett fel");
            }
        }
    }
}
