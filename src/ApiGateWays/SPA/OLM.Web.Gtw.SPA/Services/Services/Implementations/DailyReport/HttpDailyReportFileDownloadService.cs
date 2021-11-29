using Microsoft.AspNetCore.WebUtilities;
using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.DailyReport
{
    public class HttpDailyReportFileDownloadService : IDailyReportFileDownloadService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public HttpDailyReportFileDownloadService(IHttpClientFactory httpClientFactory,
                                                  ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<byte[]> DownloadDimDaily(DateTime date, IEnumerable<TramResponseViewModel> models)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var url = Endpoints.DailyReport.FileDownload.BuildDimDaily(_serviceUrls.DailyReport);

            var route = QueryHelpers.AddQueryString(url, "date", date.ToString("s"));

            var jsonText = JsonSerializer.Serialize(models);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            return await respone.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> DownloadDimWeekly(DateTime date, IEnumerable<TramResponseViewModel> models)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var url = Endpoints.DailyReport.FileDownload.BuildDimWeekly(_serviceUrls.DailyReport);

            var route = QueryHelpers.AddQueryString(url, "date", date.ToString("s"));

            var jsonText = JsonSerializer.Serialize(models);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            return await respone.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> DownloadWeekly(DateTime date, IEnumerable<TramResponseViewModel> models)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var url = Endpoints.DailyReport.FileDownload.BuildWeekly(_serviceUrls.DailyReport);

            var route = QueryHelpers.AddQueryString(url, "date", date.ToString("s"));

            var jsonText = JsonSerializer.Serialize(models);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            return await respone.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> DownloadWeeks(WeeksRequestViewModel model, IEnumerable<TramResponseViewModel> models)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var url = Endpoints.DailyReport.FileDownload.BuildWeeks(_serviceUrls.DailyReport);

            var route = QueryHelpers.AddQueryString(url,
                new Dictionary<string, string>
                {
                    { "Start", model.Start.ToString("s") },
                    { "End", model.End.ToString("s") }
                });

            var jsonText = JsonSerializer.Serialize(models);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);

            return await respone.Content.ReadAsByteArrayAsync();
        }
    }
}
