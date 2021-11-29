using OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions
{
    public class RoutingManagerForDeletingSetAction
    {
        public RoutingManagerForDeletingSetAction(RoutingDataViewModel model)
        {
            Model = model;
        }

        public RoutingDataViewModel Model { get; set; }
    }
}
