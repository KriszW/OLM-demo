using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using OLM.Services.TCO.API.Services.Services.Abstractions;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Implementations
{
    public class BundleTCOAggregatorService : IBundleTCOAggregatorService
    {
        private TCODataDbContext _dbContext;
        private ITCODataRepository _tcoDataRepository;
        private ITCOCalculatorFromBundlesService _tcoCalculatorService;
        private ITCOSettingsRepository _tcoSettingsRepository;

        public BundleTCOAggregatorService(TCODataDbContext dbContext, ITCODataRepository tcoDataRepository, ITCOCalculatorFromBundlesService tcoCalculatorService, ITCOSettingsRepository tcoSettingsRepository)
        {
            _dbContext = dbContext;
            _tcoDataRepository = tcoDataRepository;
            _tcoCalculatorService = tcoCalculatorService;
            _tcoSettingsRepository = tcoSettingsRepository;
        }

        public async Task<BundleTCOAPIResponseViewModel> AggregateDataForBundle(string bundleID)
        {
            var calculatedValue = await _tcoCalculatorService.CalculateTCO(bundleID);
            var bundleData = await _tcoDataRepository.GetByBundleID(bundleID);

            if (bundleData != default)
            {
                var settingsValues = await _tcoSettingsRepository.GetByDimension(bundleData.RawMaterialItemNumber);

                if (settingsValues != default)
                {
                    return new BundleTCOAPIResponseViewModel(calculatedValue, settingsValues.ExpectedTCOValue, settingsValues.MaximumDifference);
                } 
            }

            return default;
        }

        public async Task<BundleTCOAPIResponseViewModel> AggregateDataForBundles(IEnumerable<string> bundleIDs)
        {
            var calculatedValue = await _tcoCalculatorService.CalculateAVGTCO(bundleIDs);
            var bundles = await _tcoDataRepository.GetByBundleIDs(bundleIDs);

            var dimensions = _dbContext.TCOData.Where(b => bundleIDs.Any(id => id == b.BundleID)).Select(b => b.RawMaterialItemNumber);

            if (dimensions.Any())
            {
                var settingsValues = await _tcoSettingsRepository.GetForDimensions(dimensions);

                if (settingsValues.Any())
                {
                    var avgExpectedValue = settingsValues.Average(s => s.ExpectedTCOValue);
                    var avgMaxDifference = settingsValues.Average(s => s.MaximumDifference);

                    return new BundleTCOAPIResponseViewModel(calculatedValue, avgExpectedValue, avgMaxDifference);
                } 
            }

            return default;
        }
    }
}
