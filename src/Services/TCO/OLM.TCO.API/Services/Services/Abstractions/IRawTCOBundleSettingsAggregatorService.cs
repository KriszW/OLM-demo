using OLM.Services.TCO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Abstractions
{
    public interface IRawTCOBundleSettingsAggregatorService
    {
        double CalculateWeightedExpectedTCO(IEnumerable<TCODataModel> bundles, IEnumerable<TCOValueSettingsModel> settings);
        double CalculateWeightedMaxDifference(IEnumerable<TCODataModel> bundles, IEnumerable<TCOValueSettingsModel> settings);
    }
}
