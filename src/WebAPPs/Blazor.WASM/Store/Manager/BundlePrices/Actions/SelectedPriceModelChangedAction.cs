using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions
{
    public class SelectedPriceModelChangedAction
    {
        public SelectedPriceModelChangedAction(BundlePriceDTOViewModel newSelectedModel)
        {
            NewSelectedModel = newSelectedModel;
        }

        public BundlePriceDTOViewModel NewSelectedModel { get; private set; }
    }
}
