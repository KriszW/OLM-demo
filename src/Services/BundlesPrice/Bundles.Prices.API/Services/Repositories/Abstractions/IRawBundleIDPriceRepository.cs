using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions
{
    public interface IRawBundleIDPriceRepository
    {
        Task<BundlePriceWithBundleIDsViewModel> GetByBundleID(string bundleID);

        Task<IEnumerable<BundlePriceWithBundleIDsViewModel>> GetByBundleIDs(IEnumerable<string> bundleIDs);
    }
}
