using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions
{
    public class SelectedTramDimensionModelChangedAction
    {
        public SelectedTramDimensionModelChangedAction(TramDimensionViewModel newSelectedModel)
        {
            NewSelectedModel = newSelectedModel;
        }

        public TramDimensionViewModel NewSelectedModel { get; private set; }
    }
}
