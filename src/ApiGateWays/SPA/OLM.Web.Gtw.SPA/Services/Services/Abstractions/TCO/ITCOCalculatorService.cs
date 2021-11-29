using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO
{
    public interface ITCOCalculatorService
    {
        Task<BundleTCOAPIResponseViewModel> CalculateTCO(string bundleID);
        Task<BundleTCOAPIResponseViewModel> CalculateAVGTCO(IEnumerable<string> bundleIDs);
    }
}
