using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions
{
    public class FetchRoutingPausesManagersSuccessAction
    {
        public FetchRoutingPausesManagersSuccessAction(Paginated<WeekNumberPaginatorModelDataViewModel> model)
        {
            Model = model;
        }

        public Paginated<WeekNumberPaginatorModelDataViewModel> Model { get; private set; }
    }
}
