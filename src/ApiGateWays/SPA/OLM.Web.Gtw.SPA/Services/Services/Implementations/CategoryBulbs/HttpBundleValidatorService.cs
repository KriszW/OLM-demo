using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.CategoryBulbs;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.CategoryBulbs.APIResponses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OneOf;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.OneOfExtensions;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.CategoryBulbs
{
    public class HttpBundleValidatorService : IValidateOneBundleService
    {
        private IHttpClientFactory _httpClientFactory;
        private ServiceUrlSettings _serviceUrls;

        public HttpBundleValidatorService(IHttpClientFactory httpClientFactory, ServiceUrlSettings serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<OneOf<IEnumerable<ValidationResult>, APIError>> ValidateBundle(string bundleID)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = Endpoints.CategoryBulbs.Validator.BuildValidatorURL(_serviceUrls.CategoryBulbs, bundleID);
            var result = await client.GetWithJsonAsync<APIResponse<IEnumerable<ValidationResult>>>(route);

            return result.TryMatchModel();
        }
    }
}
