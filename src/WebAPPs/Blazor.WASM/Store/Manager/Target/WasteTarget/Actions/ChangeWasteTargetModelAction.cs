using OLM.Shared.Models.Target.SharedAPIModels;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions
{
    public class ChangeWasteTargetModelAction
    {
        public ChangeWasteTargetModelAction(WasteTargetViewModel newModelForEdit)
        {
            NewModelForEdit = newModelForEdit;
        }

        public WasteTargetViewModel NewModelForEdit { get; set; }
    }
}
