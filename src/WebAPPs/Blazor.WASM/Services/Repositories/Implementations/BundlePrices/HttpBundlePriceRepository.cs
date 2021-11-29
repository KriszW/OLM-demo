using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.BundlePrices;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.BundlePrices
{
    public class HttpBundlePriceRepository : IBundlePriceRepository
    {
        private readonly HttpClient _httpClient;

        public HttpBundlePriceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Delete(int id)
        {
            var route = APIGTWEndpoints.BundlePrices.Delete(id);

            var msg = await _httpClient.DeleteAsync(route);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public Task<APIResponse<BundlePriceDTOViewModel>> GetByID(int id)
        {
            var route = APIGTWEndpoints.BundlePrices.GetByID(id);

            return _httpClient.GetWithJsonAsync<APIResponse<BundlePriceDTOViewModel>>(route);
        }

        public Task<APIResponse<Paginated<BundlePriceDTOViewModel>>> GetPaginatedData(int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.BundlePrices.GetPaginationList(),
                new Dictionary<string, string>()
                {
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<BundlePriceDTOViewModel>>>(route);
        }

        public async Task<EmptyAPIResponse> Modify(int id, BundlePriceDTOViewModel model)
        {
            var route = APIGTWEndpoints.BundlePrices.Modify(id);

            var msg = await _httpClient.PutAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Upload(BundlePriceDTOViewModel model)
        {
            var route = APIGTWEndpoints.BundlePrices.Upload();

            var msg = await _httpClient.PostAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
