using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Target;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Target.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Target
{
    public class HttpWasteTargetManagerRepository : IWasteTargetManagerRepository
    {
        private readonly HttpClient _httpClient;

        public HttpWasteTargetManagerRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Delete(int id)
        {
            var route = APIGTWEndpoints.WasteTarget.Manager.Delete(id);

            var msg = await _httpClient.DeleteAsync(route);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public Task<APIResponse<WasteTargetViewModel>> GetByID(int id)
        {
            var route = APIGTWEndpoints.WasteTarget.Manager.GetByID(id);

            return _httpClient.GetWithJsonAsync<APIResponse<WasteTargetViewModel>>(route);
        }

        public Task<APIResponse<Paginated<WasteTargetViewModel>>> GetPaginatedData(int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.WasteTarget.Manager.GetPaginationList(),
                new Dictionary<string, string>()
                {
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<WasteTargetViewModel>>>(route);
        }

        public async Task<EmptyAPIResponse> Modify(int id, WasteTargetViewModel model)
        {
            var route = APIGTWEndpoints.WasteTarget.Manager.Modify(id);

            var msg = await _httpClient.PutAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Upload(WasteTargetViewModel model)
        {
            var route = APIGTWEndpoints.WasteTarget.Manager.Upload();

            var msg = await _httpClient.PostAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
