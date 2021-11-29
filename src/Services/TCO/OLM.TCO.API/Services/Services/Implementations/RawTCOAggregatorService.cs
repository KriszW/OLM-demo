using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using OLM.Services.TCO.API.Services.Services.Abstractions;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Implementations
{
    public class RawTCOAggregatorService : IRawTCOAggregatorService
    {
        private readonly ITCODataRepository _tcoDataRepository;
        private readonly ITCOSettingsRepository _tcoSettingsRepository;
        private readonly IRawTCOCalculatorService _rawTCOCalculatorService;
        private readonly IRawTCOBundleSettingsAggregatorService _rawTCOManyBundleSettingsAggregatorService;

        public RawTCOAggregatorService(ITCODataRepository tcoDataRepository, ITCOSettingsRepository tcoSettingsRepository, IRawTCOCalculatorService rawTCOCalculatorService, IRawTCOBundleSettingsAggregatorService rawTCOManyBundleSettingsAggregatorService)
        {
            _tcoDataRepository = tcoDataRepository;
            _tcoSettingsRepository = tcoSettingsRepository;
            _rawTCOCalculatorService = rawTCOCalculatorService;
            _rawTCOManyBundleSettingsAggregatorService = rawTCOManyBundleSettingsAggregatorService;
        }

        public async Task<BundleTCOAPIResponseViewModel> Aggregate(RawTCOQueryDataViewModel model)
        {
            if (model?.BundleIDs == default || model.BundleIDs.Any() == false) return default;

            var tcoSettings = await _tcoSettingsRepository.GetByDimension(model.ItemNumber);
            var tco = await _rawTCOCalculatorService.Calculate(model);

            if (tcoSettings == default) return default;

            return new BundleTCOAPIResponseViewModel(tco, tcoSettings.ExpectedTCOValue, tcoSettings.MaximumDifference);
        }

        public async Task<BundleTCOAPIResponseViewModel> Aggregate(IEnumerable<RawTCOQueryDataViewModel> model)
        {
            if (model == default || model.Any() == false) return default;

            var tco = await _rawTCOCalculatorService.Calculate(model);

            var bundles = await _tcoDataRepository.GetByBundleIDs(model.Select(m => m.BundleIDs).SelectMany(l => l));
            var settings = await _tcoSettingsRepository.GetForDimensions(model.Select(m => m.ItemNumber).Distinct());

            var expTCOTask = _rawTCOManyBundleSettingsAggregatorService.CalculateWeightedExpectedTCO(bundles, settings);
            var maxDiffTask = _rawTCOManyBundleSettingsAggregatorService.CalculateWeightedMaxDifference(bundles, settings);

            return new BundleTCOAPIResponseViewModel(tco, expTCOTask, maxDiffTask);
        }
    }
}
