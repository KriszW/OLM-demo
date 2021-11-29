using OLM.Shared.Models.Target.SharedAPIModels;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions
{
    public class WasteTargetModelForDeletingSetAction
    {
        public WasteTargetModelForDeletingSetAction(WasteTargetViewModel model)
        {
            Model = model;
        }

        public WasteTargetViewModel Model { get; set; }
    }
}
