using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.TCOBundle.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO;
using OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Bundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.TCOBundle.Implementations
{
    public class TCOBundleAggregator : ITCOBundleAggregator
    {
        private readonly IFetchBundleService _fetchBundleService;
        private readonly IFetchTCODataService _fetchTCODataService;

        public TCOBundleAggregator(IFetchBundleService fetchBundleService,
                                   IFetchTCODataService fetchTCODataService)
        {
            _fetchBundleService = fetchBundleService;
            _fetchTCODataService = fetchTCODataService;
        }

        public async Task<IEnumerable<TCOBundleModel>> GetDataFrom(DateTime from, DateTime to)
        {
            var output = new List<TCOBundleModel>();

            var bundles = await _fetchBundleService.GetFrom(from, to);

            if (bundles.Any() == false) return default;

            var tcoModels = await _fetchTCODataService.FetchModels(bundles.Select(b => b.BundleID));

            foreach (var bundleData in bundles)
            {
                var tcoModel = tcoModels.FirstOrDefault(b => b.BundleID == bundleData.BundleID);

                if (tcoModel != default)
                {
                    output.Add(new TCOBundleModel(bundleData, tcoModel));
                }
            }

            return output;
        }
    }
}
