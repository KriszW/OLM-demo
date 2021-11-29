using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations
{
    public class RawBundleIDPriceRepository : IRawBundleIDPriceRepository
    {
        private readonly BundlePriceDbContext _dbContext;
        private readonly IPricesRepository _priceRepository;

        public RawBundleIDPriceRepository(BundlePriceDbContext dbContext, IPricesRepository priceRepository)
        {
            _dbContext = dbContext;
            _priceRepository = priceRepository;
        }

        public async Task<BundlePriceWithBundleIDsViewModel> GetByBundleID(string bundleID)
        {
            var bundle = await _dbContext.Bundles.FirstOrDefaultAsync(m => m.BundleID == bundleID)
                         ?? throw new APIErrorException($"A '{bundleID}' rakat nincs feltöltve az adatbázisba, ezért nem kérhető le");

            var model = new BundlePriceFromItemNumberViewModel { RawMaterialItemNumber = bundle.ItemNumber, VendorID = bundle.VendorID };
            var output = await _priceRepository.GetByItemNumber(model)
                         ?? throw new APIErrorException($"A {model.RawMaterialItemNumber} cikkhez a {model.VendorID} vendoridval nincs feltöltve rakat ár a rendszerbe");

            return new BundlePriceWithBundleIDsViewModel(output.ItemNumber, output.VendorID, output.Price, new string[] { bundleID });
        }

        public async Task<IEnumerable<BundlePriceWithBundleIDsViewModel>> GetByBundleIDs(IEnumerable<string> bundleIDs)
        {
            if (bundleIDs == default || bundleIDs.Any() == false) return default;

            var bundles = _dbContext.Bundles.Where(m => bundleIDs.Any(b => m.BundleID == b));

            if (bundles == default || await bundles.AnyAsync() == false) throw new APIErrorException("A beküldött rakatok egyikéhez sincs feltöltve rakatadat a rendszerbe");

            var model = GetBundlePriceWithBundleIDsViewModels(bundles);

            var output = await _priceRepository.GetByItemNumbers(model);

            return GroupBundlePricesWithBundleIDs(bundles, output);
        }

        private IEnumerable<BundlePriceFromItemNumberViewModel> GetBundlePriceWithBundleIDsViewModels(IEnumerable<RawBundlesModel> bundles)
        {
            // Összegyűjti egyedire a beküldőtt rakatadatokból a cikk és vendorid kombókat
            var output = new HashSet<BundlePriceFromItemNumberViewModel>();

            foreach (var item in bundles)
            {
                if (item == null) continue;

                var model = output.FirstOrDefault(m => m.RawMaterialItemNumber == item.ItemNumber && m.VendorID == item.VendorID);

                if (model == default)
                {
                    output.Add(new BundlePriceFromItemNumberViewModel { RawMaterialItemNumber = item.ItemNumber, VendorID = item.VendorID });
                }
            }

            return output;
        }

        private IEnumerable<BundlePriceWithBundleIDsViewModel> GroupBundlePricesWithBundleIDs(IEnumerable<RawBundlesModel> bundles,
                                                                                              IEnumerable<BundlePriceViewModel> prices)
        {
            var output = new List<BundlePriceWithBundleIDsViewModel>();

            foreach (var item in bundles)
            {
                var price = prices.FirstOrDefault(m => m.ItemNumber == item.ItemNumber && m.VendorID == item.VendorID)
                            ?? throw new APIErrorException($"A {item.ItemNumber} cikkhez a {item.VendorID} vendoridval nincs feltöltve rakat ár a rendszerbe");

                var priceWithBundleIDs = output.FirstOrDefault(m => m.ItemNumber == item.ItemNumber && m.VendorID == item.VendorID);

                if (priceWithBundleIDs == default) output.Add(new BundlePriceWithBundleIDsViewModel(item.ItemNumber,
                    item.VendorID, price.Price, new List<string> { item.BundleID }));
                else priceWithBundleIDs.BundleIDs.Add(item.BundleID);
            }

            return output;
        }
    }
}
