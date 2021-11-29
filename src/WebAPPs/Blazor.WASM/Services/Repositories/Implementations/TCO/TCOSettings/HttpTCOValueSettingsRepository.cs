using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.TCO.TCOSettings;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.TCOSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.TCO.TCOSettings
{
    public class HttpTCOValueSettingsRepository : ITCOValueSettingsRepository
    {
        private readonly HttpClient _httpClient;

        public HttpTCOValueSettingsRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Delete(int id)
        {
            var route = APIGTWEndpoints.TCOSettings.Delete(id);

            var msg = await _httpClient.DeleteAsync(route);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public Task<APIResponse<TCOSettingsViewModel>> GetByID(int id)
        {
            var route = APIGTWEndpoints.TCOSettings.GetByID(id);

            return _httpClient.GetWithJsonAsync<APIResponse<TCOSettingsViewModel>>(route);
        }

        public Task<APIResponse<Paginated<TCOSettingsViewModel>>> GetPaginatedData(int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.TCOSettings.GetPaginationList(),
                new Dictionary<string, string>()
                {
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<TCOSettingsViewModel>>>(route);
        }

        public async Task<EmptyAPIResponse> Modify(int id, TCOSettingsViewModel model)
        {
            var route = APIGTWEndpoints.TCOSettings.Modify(id);

            var msg = await _httpClient.PutAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Upload(TCOSettingsViewModel model)
        {
            var route = APIGTWEndpoints.TCOSettings.Upload();

            var msg = await _httpClient.PostAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
