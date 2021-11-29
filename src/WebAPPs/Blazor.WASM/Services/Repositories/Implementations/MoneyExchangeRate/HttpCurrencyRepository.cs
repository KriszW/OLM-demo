using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.MoneyExchangeRate;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.MoneyExchangeRate
{
    public class HttpCurrencyRepository : ICurrencyRepository
    {
        private readonly HttpClient _httpClient;

        public HttpCurrencyRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Delete(int id)
        {
            var route = APIGTWEndpoints.CurrencyManager.Delete(id);

            var msg = await _httpClient.DeleteAsync(route);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public Task<APIResponse<List<string>>> GetAll()
        {
            var route = APIGTWEndpoints.CurrencyManager.All();

            return _httpClient.GetWithJsonAsync<APIResponse<List<string>>>(route);
        }

        public Task<APIResponse<CurrencyViewModel>> GetByID(int id)
        {
            var route = APIGTWEndpoints.CurrencyManager.GetByID(id);

            return _httpClient.GetWithJsonAsync<APIResponse<CurrencyViewModel>>(route);
        }

        public Task<APIResponse<Paginated<CurrencyViewModel>>> GetPaginatedData(int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.CurrencyManager.GetPaginationList(),
                new Dictionary<string, string>()
                {
                    { "pageIndex", pageIndex.ToString() },
                    { "pageSize", pageSize.ToString() }
                }
                );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<CurrencyViewModel>>>(route);
        }

        public async Task<EmptyAPIResponse> Modify(int id, CurrencyViewModel model)
        {
            model.Rates = new List<ExchangeRateViewModel>();

            var route = APIGTWEndpoints.CurrencyManager.Modify(id);

            var msg = await _httpClient.PutAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }

        public async Task<EmptyAPIResponse> Upload(CurrencyViewModel model)
        {
            model.Rates = new List<ExchangeRateViewModel>();

            var route = APIGTWEndpoints.CurrencyManager.Upload();

            var msg = await _httpClient.PostAsJsonAsync(route, model);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
