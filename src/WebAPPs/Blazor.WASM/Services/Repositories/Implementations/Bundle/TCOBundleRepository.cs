using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Bundle;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Bundle
{
    public class TCOBundleRepository : ITCOBundleRepository
    {
        private readonly HttpClient _httpClient;

        public TCOBundleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<APIResponse<Paginated<TCOBundleAPIResponseViewModel>>> GetData(DateTime from, DateTime to, int pageIndex, int pageSize)
        {
            var route = QueryHelpers.AddQueryString(APIGTWEndpoints.Bundles.GetPaginationList(),
                            new Dictionary<string, string>()
                            {
                                { "from", from.ToString("s") },
                                { "to", to.ToString("s") },
                                { "pageIndex", pageIndex.ToString() },
                                { "pageSize", pageSize.ToString() }
                            }
                            );

            return _httpClient.GetWithJsonAsync<APIResponse<Paginated<TCOBundleAPIResponseViewModel>>>(route);
        }
    }
}
