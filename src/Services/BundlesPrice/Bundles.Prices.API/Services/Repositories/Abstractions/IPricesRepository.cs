using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions
{
    public interface IPricesRepository
    {
        Task<BundlePriceViewModel> GetByItemNumber(BundlePriceFromItemNumberViewModel model);

        Task<IEnumerable<BundlePriceViewModel>> GetByItemNumbers(IEnumerable<BundlePriceFromItemNumberViewModel> models);
    }
}
