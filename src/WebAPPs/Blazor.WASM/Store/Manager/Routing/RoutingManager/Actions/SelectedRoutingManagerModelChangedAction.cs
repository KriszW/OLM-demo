using OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions
{
    public class SelectedRoutingManagerModelChangedAction
    {
        public SelectedRoutingManagerModelChangedAction(RoutingDataViewModel newSelectedModel)
        {
            NewSelectedModel = newSelectedModel;
        }

        public RoutingDataViewModel NewSelectedModel { get; private set; }
    }
}
