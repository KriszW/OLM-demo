using Microsoft.AspNetCore.WebUtilities;
using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.DailyReport
{
    public class HttpDailyReportService : IDailyReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public HttpDailyReportService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<DimensionReportSummarizedResponseViewModel> FechDimensionForDay(DateTime date, IEnumerable<TramResponseViewModel> tramModels)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var url = Endpoints.DailyReport.Daily.BuildDailyFetchUrl(_serviceUrls.DailyReport);

            var route = QueryHelpers.AddQueryString(url, "date", date.ToString("s"));

            var jsonText = JsonSerializer.Serialize(tramModels);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            var result = await respone.Content.ReadFromJsonAsync<APIResponse<DimensionReportSummarizedResponseViewModel>>();

            return result.TryGetModel();
        }

        public async Task<DimensionReportSummarizedResponseViewModel> FechDimensionForWeek(DateTime date, IEnumerable<TramResponseViewModel> tramModels)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var url = Endpoints.DailyReport.Daily.BuildWeekFetchUrl(_serviceUrls.DailyReport);

            var route = QueryHelpers.AddQueryString(url, "date", date.ToString("s"));

            var jsonText = JsonSerializer.Serialize(tramModels);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            var result = await respone.Content.ReadFromJsonAsync<APIResponse<DimensionReportSummarizedResponseViewModel>>();

            return result.TryGetModel();
        }
    }
}
