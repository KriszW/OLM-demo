using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions
{
    public class FetchBundlePricesFailedAction
    {
        public FetchBundlePricesFailedAction(APIError errors)
        {
            Errors = errors;
        }

        public APIError Errors { get; set; }
    }
}
