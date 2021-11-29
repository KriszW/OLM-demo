using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.TCO.API.Extensions;
using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using OLM.Services.TCO.API.Services.Services.Abstractions;
using OLM.Services.TCO.API.ViewModels;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Implementations
{
    public class EuroTCOCalculatorService : ITCOCalculatorFromBundlesService
    {
        public const int TCODefaultReturnValue = 0;

        private readonly ITCODataRepository _tcoDataRepository;
        private readonly IBundlePriceRepository _bundlePriceRepository;
        private readonly ITCOCalculationProviderService _tcoCalculationProviderService;

        public EuroTCOCalculatorService(ITCODataRepository tcoDataRepository,
                                        ITCOCalculationProviderService tcoCalculationProviderService,
                                        IBundlePriceRepository bundlePriceRepository = default)
        {
            _tcoDataRepository = tcoDataRepository;
            _bundlePriceRepository = bundlePriceRepository;
            _tcoCalculationProviderService = tcoCalculationProviderService;
        }

        public async Task<IEnumerable<TCODataAPIResponseViewModel>> GetData(IEnumerable<string> bundleIDs)
        {
            var output = new List<TCODataAPIResponseViewModel>();

            foreach (var bundleID in bundleIDs)
            {
                var model = await _tcoDataRepository.GetByBundleID(bundleID);

                if (model != default)
                {
                    var tcoTask = CalculateTCO(bundleID);
                    var standardTCOTask = CalculateStandardPriceAsync(model);
                    var matNumber = model.RawMaterialItemNumber;

                    await Task.WhenAll(tcoTask, standardTCOTask);

                    output.Add(new TCODataAPIResponseViewModel { BundleID = bundleID, ActualTCO = tcoTask.Result, StandardTCO = standardTCOTask.Result, MaterialNumber = matNumber });
                }
                else throw new APIErrorException(new APIError($"A {bundleID} rakat nem található az adatbázisban"));
                
            }

            return output;
        }

        private async Task<double> CalculateStandardPriceAsync(TCODataModel model)
        {
            var price = await _bundlePriceRepository.GetPrice(new BundlePriceFromItemNumberViewModel { RawMaterialItemNumber = model.RawMaterialItemNumber, VendorID = model.VendorID });
            if (price == default) return 0.0;

            var sTCO = (double)price.Price / model.CalculateGoodProducts();
            return double.IsNaN(sTCO) == false ? sTCO : 0.0;
        }

        public async Task<double> CalculateTCO(string bundleID)
        {
            var data = await _tcoDataRepository.GetByBundleID(bundleID);

            if (data == default) throw new APIErrorException(new APIError($"A {bundleID} rakat nem szerepel az adatbázisban"));

            var price = await _bundlePriceRepository.GetPrice(new BundlePriceFromItemNumberViewModel { RawMaterialItemNumber = data.RawMaterialItemNumber, VendorID = data.VendorID });

            if (price == default) throw new APIErrorException(new APIError($"A {data.RawMaterialItemNumber} cikkhez és a {data.VendorID} beszállítóhoz nincs rakat ár feltöltve"));

            return _tcoCalculationProviderService.Calculate(data.CreateTCOViewModel(price));
        }

        public async Task<double> CalculateAVGTCO(IEnumerable<string> bundleIDs)
        {
            if (bundleIDs != default && bundleIDs.Any() == true)
            {
                var bundles = await _tcoDataRepository.GetByBundleIDs(bundleIDs);

                return await CalcTCOForBundlesAsync(bundles);
            }

            return TCODefaultReturnValue;
        }

        private async Task<double> CalcTCOForBundlesAsync(List<TCODataModel> bundles)
        {
            var data = bundles.Select(b => new BundlePriceFromItemNumberViewModel() { RawMaterialItemNumber = b.RawMaterialItemNumber, VendorID = b.VendorID }).Distinct();

            var prices = await _bundlePriceRepository.GetPrices(data);

            return CalcAVGTCO(GroupBundlesWithPrices(bundles, prices));
        }

        private List<TCOBundleSummirezed> GroupBundlesWithPrices(List<TCODataModel> bundles, IEnumerable<BundlePriceViewModel> prices)
        {
            var output = new List<TCOBundleSummirezed>();

            foreach (var bundle in bundles)
            {
                var summedModel = output.FirstOrDefault(s => s?.Price?.ItemNumber == bundle?.RawMaterialItemNumber);

                if (summedModel == default)
                {
                    var newModel = CreateNewModel(prices, bundle);
                    output.Add(newModel);
                }
                else
                {
                    summedModel.TCOData.Add(bundle);
                }
            }

            return output;
        }

        private TCOBundleSummirezed CreateNewModel(IEnumerable<BundlePriceViewModel> prices, TCODataModel bundle)
        {
            var price = prices.FirstOrDefault(p => p.ItemNumber == bundle.RawMaterialItemNumber);

            var output = new TCOBundleSummirezed(price);
            output.TCOData.Add(bundle);

            return output;
        }

        private double CalcAVGTCO(List<TCOBundleSummirezed> groupedData)
        {
            if (groupedData != default && groupedData.Any())
            {
                var tcos = CalculateTCOsInParallel(groupedData);

                return tcos.Average();
            }

            return TCODefaultReturnValue;
        }

        private List<double> CalculateTCOsInParallel(List<TCOBundleSummirezed> groupedData)
        {
            var tcos = new List<double>();

            var res = Parallel.ForEach(groupedData, (item) =>
            {
                if (item.Price != default && item.TCOData != default)
                {
                    var model = new TCOCalculationViewModel(item.TCOData.Sum(td => td.Volume),
                                                            (double)item?.Price?.Price,
                                                            item.TCOData.Sum(td => td.Primary + td.Secondary));

                    tcos.Add(_tcoCalculationProviderService.Calculate(model));
                }
                else
                {
                    var model = item.TCOData?.FirstOrDefault();

                    if (model != default) 
                        throw new APIErrorException(
                            new APIError($"A {model.RawMaterialItemNumber} cikk {model.VendorID} vendorral nincs ár feltöltve")
                        );
                }
            });

            return tcos;
        }
    }
}
