using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Abstractions
{
    public interface IRawTCOAggregatorService
    {
        Task<BundleTCOAPIResponseViewModel> Aggregate(RawTCOQueryDataViewModel model);

        Task<BundleTCOAPIResponseViewModel> Aggregate(IEnumerable<RawTCOQueryDataViewModel> model);
    }
}
