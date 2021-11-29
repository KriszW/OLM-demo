using Microsoft.AspNetCore.WebUtilities;
using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
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
    public class HttpWeeksReportService : IWeeksReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public HttpWeeksReportService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<WeeksReportResponseViewModel> FetchYearlyWeeks(WeeksRequestViewModel model, IEnumerable<TramResponseViewModel> tramModels)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var url = Endpoints.DailyReport.Weeks.BuildWeeks(_serviceUrls.DailyReport);

            var route = QueryHelpers.AddQueryString(url,
                new Dictionary<string, string>
                {
                    { "Start", model.Start.ToString("s") },
                    { "End", model.End.ToString("s") }
                });

            var jsonText = JsonSerializer.Serialize(tramModels);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            var result = await respone.Content.ReadFromJsonAsync<APIResponse<WeeksReportResponseViewModel>>();

            return result.TryGetModel();
        }
    }
}
