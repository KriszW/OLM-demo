using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Tram
{
    public class HttpTramDimensionRepository : ITramDimensionRepository
    {
        private readonly HttpClient _httpClient;

        public HttpTramDimensionRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Delete(int id)
        {
            var route = APIGTWEndpoints.Tram.DimensionManager.Delete(id);

            var msg = await _httpClient.DeleteAsync(route);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public Task<APIResponse<IEnumerable<string>>> GetAllDimensions()
        {
            var route = APIGTWEndpoints.Tram.DimensionManager.GetAll();

            return _httpClient.GetWithJsonAsync<APIResponse<IEnumerable<string>>>(route);
        }

        public Task<APIResponse<TramDimensionViewModel>> GetByID(int id)
        {
            var route = APIGTWEndpoints.Tram.DimensionManager.GetByID(id);

            return _httpClient.GetWithJsonAsync<APIResponse<TramDimensionViewModel>>(route);
        }

        public Task<APIResponse<Paginated<TramDimensionViewModel>>> GetPaginatedData(int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.Tram.DimensionManager.GetPaginationList(),
                new Dictionary<string, string>()
                {
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<TramDimensionViewModel>>>(route);
        }

        public async Task<EmptyAPIResponse> Modify(int id, TramDimensionViewModel model)
        {
            var route = APIGTWEndpoints.Tram.DimensionManager.Modify(id);

            var msg = await _httpClient.PutAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Upload(TramDimensionViewModel model)
        {
            var route = APIGTWEndpoints.Tram.DimensionManager.Upload();

            var msg = await _httpClient.PostAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
