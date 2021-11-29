using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions
{
    public class FetchProdTimesFailedAction
    {
        public FetchProdTimesFailedAction(APIError errors)
        {
            Errors = errors;
        }

        public APIError Errors { get; set; }
    }
}
