using Microsoft.AspNetCore.WebUtilities;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Weekly;
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

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Weekly
{
    public class HttpWeeklyDayWasteFollowUpService : IWeeklyDayFollowUpRepository
    {
        private readonly HttpClient _httpClient;

        public HttpWeeklyDayWasteFollowUpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<APIResponse<WeeklyReportResponseViewModel>> FetchDayReport(DateTime date)
        {
            var url = APIGTWEndpoints.DailyReport.FetchWeeklyDay();

            var route = QueryHelpers.AddQueryString(url, "date", date.ToString("s"));

            return _httpClient.GetWithJsonAsync<APIResponse<WeeklyReportResponseViewModel>>(route);
        }
    }
}
