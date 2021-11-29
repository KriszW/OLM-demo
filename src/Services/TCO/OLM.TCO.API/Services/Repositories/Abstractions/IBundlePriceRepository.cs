using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Repositories.Abstractions
{
    public interface IBundlePriceRepository
    {
        Task<BundlePriceViewModel> GetPrice(BundlePriceFromItemNumberViewModel model);

        Task<IEnumerable<BundlePriceViewModel>> GetPrices(IEnumerable<BundlePriceFromItemNumberViewModel> queryData);
    }
}
