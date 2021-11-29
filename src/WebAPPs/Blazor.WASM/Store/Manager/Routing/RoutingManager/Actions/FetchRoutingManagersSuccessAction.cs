using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions
{
    public class FetchRoutingManagersSuccessAction
    {
        public FetchRoutingManagersSuccessAction(Paginated<RoutingDataViewModel> model)
        {
            Model = model;
        }

        public Paginated<RoutingDataViewModel> Model { get; private set; }
    }
}
