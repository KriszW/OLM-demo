using Microsoft.AspNetCore.WebUtilities;
using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Bundle;
using OLM.Services.SharedBases.Responses;
using System.Net.Http.Json;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Implementations
{
    public class TCOSourceBundlesRepository : ITCOSourceBundlesRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrls _serviceUrls;

        public TCOSourceBundlesRepository(IHttpClientFactory httpClientFactory,
                                               ServiceUrls serviceUrls)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls;
        }

        public async Task<IEnumerable<TCOBundleModel>> QueryBundlesFromDate(DateTime from, DateTime to)
        {
            var client = _httpClientFactory.CreateClient();

            var url = _serviceUrls.SPAGtw + $"api/tcobundle?from={from:s}&to={to:s}";

            var result = await client.GetFromJsonAsync<APIResponse<IEnumerable<TCOBundleModel>>>(url);

            return result.TryGetModel();
        }
    }
}
