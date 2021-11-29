using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions
{
    public class FetchWasteTargetsFailedAction
    {
        public FetchWasteTargetsFailedAction(APIError errors)
        {
            Errors = errors;
        }

        public APIError Errors { get; set; }
    }
}
