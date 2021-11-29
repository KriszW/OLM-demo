using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO
{
    public interface IFetchRawTCOCalculatorService
    {
        Task<OneOf<BundleTCOAPIResponseViewModel, APIError>> CalculateTCO(RawTCOQueryDataViewModel model);
        Task<OneOf<BundleTCOAPIResponseViewModel, APIError>> CalculateAVGTCO(IEnumerable<RawTCOQueryDataViewModel> model);
    }
}
