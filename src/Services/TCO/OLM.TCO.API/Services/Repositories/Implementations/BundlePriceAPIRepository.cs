using OLM.Services.SharedBases.Responses;
using OLM.Services.TCO.API.Extensions;
using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using OLM.Services.TCO.API.ViewModels.Settings;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OLM.Shared.Extensions.HttpMessage.URIHelpers;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace OLM.Services.TCO.API.Services.Repositories.Implementations
{
    public class BundlePriceAPIRepository : IBundlePriceRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrls _serviceUrls;

        public BundlePriceAPIRepository(IHttpClientFactory httpClientFactory,
                                        ServiceUrls serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<BundlePriceViewModel> GetPrice(BundlePriceFromItemNumberViewModel model)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = _serviceUrls.BundlesPrices + $"api/prices/itemnumber/{model.RawMaterialItemNumber}/vendor/{model.VendorID}";

            var result = await client.GetWithJsonAsync<APIResponse<BundlePriceViewModel>>(route);

            return result.TryGetModel();
        }

        public async Task<IEnumerable<BundlePriceViewModel>> GetPrices(IEnumerable<BundlePriceFromItemNumberViewModel> queryData)
        {
            var client = _httpClientFactory.CreateClientForServices();
            var route = _serviceUrls.BundlesPrices + $"api/prices/itemnumbers/vendors";

            var jsonText = JsonSerializer.Serialize(queryData);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = new StringContent(jsonText, Encoding.UTF8, "application/json"),
            };

            var respone = await client.SendAsync(request);
            var result = await respone.Content.ReadFromJsonAsync<APIResponse<IEnumerable<BundlePriceViewModel>>>();

            return result.TryGetModel();
        }
    }
}
