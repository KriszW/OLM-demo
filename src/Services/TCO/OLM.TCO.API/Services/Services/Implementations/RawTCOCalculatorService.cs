using OLM.Services.TCO.API.Extensions;
using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using OLM.Services.TCO.API.Services.Services.Abstractions;
using OLM.Services.TCO.API.ViewModels;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Implementations
{
    public class RawTCOCalculatorService : IRawTCOCalculatorService
    {
        private readonly ITCODataRepository _tcoDataRepository;
        private readonly ITCOCalculationProviderService _tcoCalculationProviderService;

        public RawTCOCalculatorService(ITCODataRepository tcoDataRepository, ITCOCalculationProviderService tcoCalculationProviderService)
        {
            _tcoDataRepository = tcoDataRepository;
            _tcoCalculationProviderService = tcoCalculationProviderService;
        }

        public async Task<double> Calculate(RawTCOQueryDataViewModel model)
        {
            if (model?.BundleIDs == default || model.BundleIDs.Any() == false) return default;

            var bundle = await _tcoDataRepository.GetByBundleID(model.BundleIDs.FirstOrDefault());

            if (bundle == default) return default;

            return _tcoCalculationProviderService.Calculate(bundle.CreateTCOViewModel(model.Price));
        }

        public async Task<double> Calculate(IEnumerable<RawTCOQueryDataViewModel> models)
        {
            if (models == default || models.Any() == false) return default;

            var bundles = await _tcoDataRepository.GetByBundleIDs(models.Select(m => m.BundleIDs).SelectMany(l => l));

            if (bundles == default) return default;

            var matNumGroupedByData = GroupBundlesByItemnumber(bundles);

            return CalculateGroupedByMatNumData(matNumGroupedByData, models);
        }

        private IEnumerable<IGrouping<string, TCODataModel>> GroupBundlesByItemnumber(IEnumerable<TCODataModel> bundles) 
            => bundles.GroupBy(m => m.RawMaterialItemNumber);

        private double CalculateGroupedByMatNumData(IEnumerable<IGrouping<string, TCODataModel>> data, IEnumerable<RawTCOQueryDataViewModel> models)
        {
            var allVolume = data.Sum(d => d.Sum(m => m.Volume));

            var tcos = new List<double>();

            foreach (var group in data)
            {
                var groupedByVendor = group.GroupBy(m => m.VendorID);

                foreach (var vendorGroup in groupedByVendor)
                {
                    var allVendorVolume = vendorGroup.Sum(m => m.Volume);
                    var allVendorGoodProducts = vendorGroup.Sum(m => m.CalculateGoodProducts());

                    var price = models.FirstOrDefault(m => m.VendorID == vendorGroup.Key && m.ItemNumber == group.Key);

                    if (price == default) continue;

                    var weight = allVendorVolume / allVolume;

                    var vendorTCO = _tcoCalculationProviderService.Calculate(new TCOCalculationViewModel(allVendorVolume, (double)price.Price, allVendorGoodProducts));
                    tcos.Add(vendorTCO * weight);
                }
            }

            return tcos.Sum();
        }
    }
}
