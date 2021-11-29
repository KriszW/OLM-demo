using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Dimension;
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

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Dimension
{
    public class HttpDimensionFollowUpRepository : IDimensionFollowUpRepository
    {
        private readonly HttpClient _httpClient;

        public HttpDimensionFollowUpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<APIResponse<DimensionReportSummarizedResponseViewModel>> FetchDayReport(DateTime date)
        {
            var url = APIGTWEndpoints.DailyReport.FetchDimForDay();

            var route = QueryHelpers.AddQueryString(url, "date", date.ToString("s"));

            return _httpClient.GetWithJsonAsync<APIResponse<DimensionReportSummarizedResponseViewModel>>(route);
        }

        public Task<APIResponse<DimensionReportSummarizedResponseViewModel>> FetchWeeklyReport(DateTime date)
        {
            var url = APIGTWEndpoints.DailyReport.FetchDimForWeek();

            var route = QueryHelpers.AddQueryString(url, "date", date.ToString("s"));

            return _httpClient.GetWithJsonAsync<APIResponse<DimensionReportSummarizedResponseViewModel>>(route);
        }
    }
}
