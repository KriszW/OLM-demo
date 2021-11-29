using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Implementations
{
    public class RawTCOBundleSettingsAggregatorService : IRawTCOBundleSettingsAggregatorService
    {
        private double CalculateAllVolume(IEnumerable<TCODataModel> bundles)
            => bundles.Sum(m => m.Volume);

        public double CalculateWeightedExpectedTCO(IEnumerable<TCODataModel> bundles, IEnumerable<TCOValueSettingsModel> settings)
        {
            if (bundles == default || settings == default) return default;

            var output = 0.0;

            var groupedBundles = bundles.GroupBy(m => m.RawMaterialItemNumber);

            var allVolume = CalculateAllVolume(bundles);

            foreach (var item in settings)
            {
                if (item == default) continue;

                var itemBundles = groupedBundles.FirstOrDefault(m => m.Key == item.RawMaterialItemNumber);

                if (itemBundles == default) continue;

                var groupedVolume = itemBundles.Sum(m => m.Volume);

                var weight = groupedVolume / allVolume;

                output += item.ExpectedTCOValue * weight;
            }

            return output;
        }

        public double CalculateWeightedMaxDifference(IEnumerable<TCODataModel> bundles, IEnumerable<TCOValueSettingsModel> settings)
        {
            if (bundles == default || settings == default) return default;

            var output = 0.0;

            var groupedBundles = bundles.GroupBy(m => m.RawMaterialItemNumber);

            var allVolume = CalculateAllVolume(bundles);

            foreach (var item in settings)
            {
                if (item == default) continue;

                var itemBundles = groupedBundles.FirstOrDefault(m => m.Key == item.RawMaterialItemNumber);

                if (itemBundles == default) continue;

                var groupedVolume = itemBundles.Sum(m => m.Volume);

                var weight = groupedVolume / allVolume;

                output += item.MaximumDifference * weight;
            }

            return output;
        }
    }
}
