using Ocelot.Responses;
using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using OLM.Services.SharedBases.APIErrors;
using OneOf;
using OLM.Shared.Extensions.OneOfExtensions;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.Bundle
{
    public class FetchOneMachinesBundleWithHttpClientService : IFetchOneMachinesBundleService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public FetchOneMachinesBundleWithHttpClientService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<OneOf<BundleAPIResponseViewModel, APIError>> FetchLatestBundle(string machineName)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.Bundles.Bundle.GetLatestBundleURL(_serviceUrls.Bundles, machineName);
            var result = await client.GetWithJsonAsync<APIResponse<BundleAPIResponseViewModel>>(route);

            return result.TryMatchModel();
        }

        public async Task<OneOf<SummarizedBundlesForMachineDataViewModel, APIError>> FetchDailyBundle(string machineName)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.Bundles.MachineSummarizedData.GetDailyBundleURL(_serviceUrls.Bundles, machineName);
            var result = await client.GetWithJsonAsync<APIResponse<SummarizedBundlesForMachineDataViewModel>>(route);

            return result.TryMatchModel();
        }

        public async Task<OneOf<SummarizedBundlesForMachineDataViewModel, APIError>> FetchWeeklyBundle(string machineName)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.Bundles.MachineSummarizedData.GetWeeklyBundleURL(_serviceUrls.Bundles, machineName);
            var result = await client.GetWithJsonAsync<APIResponse<SummarizedBundlesForMachineDataViewModel>>(route);

            return result.TryMatchModel();
        }
    }
}
