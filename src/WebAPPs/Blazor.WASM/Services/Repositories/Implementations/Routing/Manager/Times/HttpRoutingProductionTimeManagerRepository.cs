using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager.Times;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Routing.Manager.Times
{
    public class HttpRoutingProductionTimeManagerRepository : IRoutingProductionTimeManagerRepository
    {
        private readonly HttpClient _httpClient;

        public HttpRoutingProductionTimeManagerRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<APIResponse<ProductionTimeDataViewModel>> GetByID(int id)
        {
            var route = APIGTWEndpoints.RoutingTimes.ProdTimeManager.GetByID(id);

            return _httpClient.GetWithJsonAsync<APIResponse<ProductionTimeDataViewModel>>(route);
        }

        public Task<APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>> GetDataPaginatedForWeeks(int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.RoutingTimes.ProdTimeManager.GetPaginationListOfWeeks(),
                new Dictionary<string, string>()
                {
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>>(route);
        }

        public Task<APIResponse<Paginated<ProductionTimeDataViewModel>>> GetPaginatedData(int year, int weekNumber, int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.RoutingTimes.ProdTimeManager.GetPaginationList(),
                new Dictionary<string, string>()
                {
                    { "year", year.ToString() },
                    { "weekNumber", weekNumber.ToString() },
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<ProductionTimeDataViewModel>>>(route);
        }

        public async Task<EmptyAPIResponse> Modify(int id, ProductionTimeDataViewModel model)
        {
            var route = APIGTWEndpoints.RoutingTimes.ProdTimeManager.Modify(id);

            var msg = await _httpClient.PutAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Upload(ProductionTimeDataViewModel model)
        {
            var route = APIGTWEndpoints.RoutingTimes.ProdTimeManager.Upload();

            var msg = await _httpClient.PostAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Delete(int id)
        {
            var route = APIGTWEndpoints.RoutingTimes.ProdTimeManager.Delete(id);

            var msg = await _httpClient.DeleteAsync(route);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
