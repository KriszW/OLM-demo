using OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions
{
    public class ChangeRoutingManagerModelAction
    {
        public ChangeRoutingManagerModelAction(RoutingDataViewModel newModelForEdit)
        {
            NewModelForEdit = newModelForEdit;
        }

        public RoutingDataViewModel NewModelForEdit { get; set; }
    }
}
