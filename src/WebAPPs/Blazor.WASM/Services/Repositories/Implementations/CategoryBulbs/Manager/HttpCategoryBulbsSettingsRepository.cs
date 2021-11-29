using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.CategoryBulbs.Manager;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.CategoryBulbs.APIResponses.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.CategoryBulbs.Manager
{
    public class HttpCategoryBulbsSettingsRepository : ICategoryBulbsSettingsRepository
    {
        private readonly HttpClient _httpClient;

        public HttpCategoryBulbsSettingsRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Delete(int id)
        {
            var route = APIGTWEndpoints.CategoryBulbs.ItemNumberManager.Delete(id);

            var msg = await _httpClient.DeleteAsync(route);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public Task<APIResponse<CategoryBulbItemNumberSettingsViewModel>> GetByID(int id)
        {
            var route = APIGTWEndpoints.CategoryBulbs.ItemNumberManager.GetByID(id);

            return _httpClient.GetWithJsonAsync<APIResponse<CategoryBulbItemNumberSettingsViewModel>>(route);
        }

        public Task<APIResponse<Paginated<CategoryBulbItemNumberSettingsViewModel>>> Search(string categoryQuery, int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.CategoryBulbs.ItemNumberManager.Search(),
            new Dictionary<string, string>()
                {
                    { "categoryQuery", categoryQuery },
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
            );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<CategoryBulbItemNumberSettingsViewModel>>>(route);
        }

        public Task<APIResponse<Paginated<CategoryBulbItemNumberSettingsViewModel>>> GetPaginatedData(int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.CategoryBulbs.ItemNumberManager.GetPaginationList(),
                new Dictionary<string, string>()
                {
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<CategoryBulbItemNumberSettingsViewModel>>>(route);
        }

        public async Task<EmptyAPIResponse> Modify(int id, CategoryBulbItemNumberSettingsViewModel model)
        {
            var route = APIGTWEndpoints.CategoryBulbs.ItemNumberManager.Modify(id);

            var msg = await _httpClient.PutAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Upload(CategoryBulbItemNumberSettingsViewModel model)
        {
            var route = APIGTWEndpoints.CategoryBulbs.ItemNumberManager.Upload();

            var msg = await _httpClient.PostAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
