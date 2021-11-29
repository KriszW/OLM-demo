using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Abstractions
{
    public interface IBundleTCOAggregatorService
    {
        Task<BundleTCOAPIResponseViewModel> AggregateDataForBundle(string bundleID);
        Task<BundleTCOAPIResponseViewModel> AggregateDataForBundles(IEnumerable<string> bundleIDs);
    }
}
