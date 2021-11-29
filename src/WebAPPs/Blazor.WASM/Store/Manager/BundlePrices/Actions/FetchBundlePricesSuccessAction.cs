using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions
{
    public class FetchBundlePricesSuccessAction
    {
        public FetchBundlePricesSuccessAction(Paginated<BundlePriceDTOViewModel> model)
        {
            Model = model;
        }

        public Paginated<BundlePriceDTOViewModel> Model { get; private set; }
    }
}
