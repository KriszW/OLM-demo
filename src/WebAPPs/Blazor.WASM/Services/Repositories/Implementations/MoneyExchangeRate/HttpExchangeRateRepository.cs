using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.MoneyExchangeRate;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.ExchangeRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.MoneyExchangeRate
{
    public class HttpExchangeRateRepository : IExchangeRateRepository
    {
        private readonly HttpClient _httpClient;

        public HttpExchangeRateRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Delete(int id)
        {
            var route = APIGTWEndpoints.ExchangeRateManager.Delete(id);

            var msg = await _httpClient.DeleteAsync(route);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public Task<APIResponse<ExchangeRateViewModel>> GetByID(int id)
        {
            var route = APIGTWEndpoints.ExchangeRateManager.GetByID(id);

            return _httpClient.GetWithJsonAsync<APIResponse<ExchangeRateViewModel>>(route);
        }

        public Task<APIResponse<Paginated<ExchangeRateViewModel>>> GetPaginatedData(string isoCode, int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.ExchangeRateManager.GetPaginationList(),
                new Dictionary<string, string>()
                {
                    { "ISOCode", isoCode },
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<ExchangeRateViewModel>>>(route);
        }

        public async Task<EmptyAPIResponse> Modify(string sourceISOCode, ExchangeRateViewModel model)
        {
            var route = APIGTWEndpoints.ExchangeRateManager.Modify(sourceISOCode);

            var msg = await _httpClient.PutAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Upload(UploadNewExchangeRateForISOCodeViewModel model)
        {
            var route = APIGTWEndpoints.ExchangeRateManager.Upload();

            var msg = await _httpClient.PostAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
