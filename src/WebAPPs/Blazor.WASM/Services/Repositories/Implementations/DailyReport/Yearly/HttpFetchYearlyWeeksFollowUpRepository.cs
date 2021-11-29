using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Yearly;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Yearly
{
    public class HttpFetchYearlyWeeksFollowUpRepository : IYearlyWeeksFollowUpRepository
    {
        private readonly HttpClient _httpClient;

        public HttpFetchYearlyWeeksFollowUpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<APIResponse<WeeksReportResponseViewModel>> Fetch(DateTime start, DateTime end)
        {
            var url = APIGTWEndpoints.DailyReport.FetchWeeks();

            var route = QueryHelpers.AddQueryString(url,
                new Dictionary<string, string> 
                {
                    { "Start", start.ToString("s") }, 
                    { "End", end.ToString("s") }, 
                });

            return _httpClient.GetWithJsonAsync<APIResponse<WeeksReportResponseViewModel>>(route);
        }
    }
}
