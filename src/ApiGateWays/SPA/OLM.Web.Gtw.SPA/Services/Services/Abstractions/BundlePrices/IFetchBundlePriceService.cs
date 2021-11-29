using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.BundlePrices
{
    public interface IFetchBundlePriceService
    {
        Task<OneOf<RawTCOQueryDataViewModel, APIError>> Fetch(string bundleID);
        Task<OneOf<IEnumerable<RawTCOQueryDataViewModel>, APIError>> Fetch(IEnumerable<string> bundleID);

        //Task<BundlePriceWithBundleIDsViewModel> Fetch(string bundleID);
        //Task<IEnumerable<BundlePriceWithBundleIDsViewModel>> Fetch(IEnumerable<string> bundleID);
    }
}
