using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Abstractions
{
    public interface IRawTCOCalculatorService
    {
        Task<double> Calculate(RawTCOQueryDataViewModel model);
        Task<double> Calculate(IEnumerable<RawTCOQueryDataViewModel> models);
    }
}
