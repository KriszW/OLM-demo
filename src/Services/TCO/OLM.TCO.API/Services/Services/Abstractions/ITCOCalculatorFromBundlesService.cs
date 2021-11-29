using OLM.Services.TCO.API.ViewModels;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Abstractions
{
    public interface ITCOCalculatorFromBundlesService
    {
        Task<IEnumerable<TCODataAPIResponseViewModel>> GetData(IEnumerable<string> bundleIDs);

        Task<double> CalculateTCO(string bundleID);
        Task<double> CalculateAVGTCO(IEnumerable<string> bundleIDs);
    }
}
