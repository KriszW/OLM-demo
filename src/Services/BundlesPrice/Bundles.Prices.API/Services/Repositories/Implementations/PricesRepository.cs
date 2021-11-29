using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations
{
    public class PricesRepository : IPricesRepository
    {
        private readonly BundlePriceDbContext _dbContext;

        public PricesRepository(BundlePriceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BundlePriceViewModel> GetByItemNumber(BundlePriceFromItemNumberViewModel model)
        {
            var data = await _dbContext.BundlePrices.FirstOrDefaultAsync(b => b.RawMaterialItemNumber == model.RawMaterialItemNumber && b.VendorID == model.VendorID);

            return data != default
                ? new BundlePriceViewModel(data.RawMaterialItemNumber, data.VendorID, data.Price)
                : throw new APIErrorException($"A {model.RawMaterialItemNumber} cikkhez a {model.VendorID} vendoridval nincs feltöltve rakat ár a rendszerbe");
        }

        public async Task<IEnumerable<BundlePriceViewModel>> GetByItemNumbers(IEnumerable<BundlePriceFromItemNumberViewModel> models)
        {
            var output = new List<BundlePriceViewModel>();

            foreach (var item in models)
            {
                var data = await _dbContext.BundlePrices.FirstOrDefaultAsync(m => m.RawMaterialItemNumber == item.RawMaterialItemNumber && m.VendorID == item.VendorID);

                if (data != default) output.Add(new BundlePriceViewModel(data.RawMaterialItemNumber, data.VendorID, data.Price));
                else throw new APIErrorException($"A {item.RawMaterialItemNumber} cikkhez a {item.VendorID} vendoridval nincs feltöltve rakat ár a rendszerbe");
            }

            return output;
        }
    }
}
