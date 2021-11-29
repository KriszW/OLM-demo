using Microsoft.AspNetCore.Http;
using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.DailyReport
{
    public class HttpBundlePriceFileUploadService : IBundlePriceFileUploadService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public HttpBundlePriceFileUploadService(IHttpClientFactory httpClientFactory,
                                                ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<EmptyAPIResponse> Upload(IFormFile file, BundlePriceFileUploadViewModel model)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.BundlePrices.Upload.BuildForUpload(_serviceUrls.BundlePrices) + $"?Rate={model.Rate}&Currency={model.Currency}";

            if (model.Rate == 0) return default;

            using var fileStream = file.OpenReadStream();

            var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))
            {
                { new StreamContent(fileStream), "File", "data.csv" },
            };


            var respone = await client.PostAsJsonAsync(route, content);

            return await respone.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
