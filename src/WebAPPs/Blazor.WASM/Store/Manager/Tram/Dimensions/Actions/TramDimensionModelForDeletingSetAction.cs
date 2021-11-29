using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions
{
    public class TramDimensionModelForDeletingSetAction
    {
        public TramDimensionModelForDeletingSetAction(TramDimensionViewModel model)
        {
            Model = model;
        }

        public TramDimensionViewModel Model { get; set; }
    }
}
