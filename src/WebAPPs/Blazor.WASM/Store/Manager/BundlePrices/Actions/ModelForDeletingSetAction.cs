using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions
{
    public class ModelForDeletingSetAction
    {
        public ModelForDeletingSetAction(BundlePriceDTOViewModel model)
        {
            Model = model;
        }

        public BundlePriceDTOViewModel Model { get; set; }
    }
}
