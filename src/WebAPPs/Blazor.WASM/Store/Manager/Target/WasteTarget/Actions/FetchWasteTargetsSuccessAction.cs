using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Target.SharedAPIModels;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions
{
    public class FetchWasteTargetsSuccessAction
    {
        public FetchWasteTargetsSuccessAction(Paginated<WasteTargetViewModel> model)
        {
            Model = model;
        }

        public Paginated<WasteTargetViewModel> Model { get; private set; }
    }
}
