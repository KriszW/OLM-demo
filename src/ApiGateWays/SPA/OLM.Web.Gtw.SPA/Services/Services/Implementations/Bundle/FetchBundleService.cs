using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.Bundle
{
    public class FetchBundleService : IFetchBundleService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrlSettings _serviceUrls;

        public FetchBundleService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<IEnumerable<BundleWithDateAPIResponseViewModel>> GetFrom(DateTime from, DateTime to)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.Bundles.Bundle.GetBundles(_serviceUrls.Bundles, from, to);
            var result = await client.GetWithJsonAsync<APIResponse<IEnumerable<BundleWithDateAPIResponseViewModel>>>(route);

            return result.TryGetModel();
        }
    }
}
