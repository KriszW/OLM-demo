using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OneOf;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.OneOfExtensions;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.Bundle
{
    public class FetchSummarizedBundleWithHttpClientService : IFetchSummarizedBundleService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public FetchSummarizedBundleWithHttpClientService(IHttpClientFactory httpClientFactory,
                                                          ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<OneOf<SummarizedDataForMachinesViewModel, APIError>> FetchDaily()
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.Bundles.MachinesSummarizedData.GetDailyBundleURL(_serviceUrls.Bundles);
            var result = await client.GetWithJsonAsync<APIResponse<SummarizedDataForMachinesViewModel>>(route);

            return result.TryMatchModel();
        }

        public async Task<OneOf<SummarizedDataForMachinesViewModel, APIError>> FetchWeekly()
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.Bundles.MachinesSummarizedData.GetWeeklyBundleURL(_serviceUrls.Bundles);
            var result = await client.GetWithJsonAsync<APIResponse<SummarizedDataForMachinesViewModel>>(route);

            return result.TryMatchModel();
        }
    }
}
