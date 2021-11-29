using Microsoft.Extensions.Logging;
using OLM.Services.DailyReport.API.Extensions;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Services.DailyReport.API.ViewModels.Settings;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpMessage.URIHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;

namespace OLM.Services.DailyReport.API.Services.Repositories.Implementations
{
    public class HttpFetchTargetRepository : ITargetRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrls _serviceUrls;
        private readonly ILogger<HttpFetchTargetRepository> _logger;

        public HttpFetchTargetRepository(IHttpClientFactory httpClientFactory,
                                         ServiceUrls serviceUrls,
                                         ILogger<HttpFetchTargetRepository> logger)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
            _logger = logger;
        }

        public async Task<TargetResponseViewModel> GetForDimension(string dimension)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var requestURI = _serviceUrls.Target + $"api/target/dimension/{dimension}";

            var encodedQuery = Uri.EscapeUriString(requestURI);
            _logger.LogInformation($"Query string:'{encodedQuery}'");

            var result = await client.GetWithJsonAsync<APIResponse<TargetResponseViewModel>>(encodedQuery);

            return result.TryGetModel();
        }

        public async Task<IEnumerable<TargetResponseViewModel>> GetForDimension(IEnumerable<string> dimensions)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var queryString = dimensions.ToOneLineQueryString("dims");
            var requestURI = _serviceUrls.Target + $"api/target/dimensions?{queryString}";

            var encodedQuery = Uri.EscapeUriString(requestURI);
            _logger.LogInformation($"Query string:'{encodedQuery}'");

            var result = await client.GetWithJsonAsync<APIResponse<IEnumerable<TargetResponseViewModel>>>(encodedQuery);

            return result.TryGetModel();
        }
    }
}
