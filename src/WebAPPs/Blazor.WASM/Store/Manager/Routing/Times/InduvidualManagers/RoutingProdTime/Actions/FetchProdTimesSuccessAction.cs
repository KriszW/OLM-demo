using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions
{
    public class FetchProdTimesSuccessAction
    {
        public FetchProdTimesSuccessAction(Paginated<ProductionTimeDataViewModel> model)
        {
            Model = model;
        }

        public Paginated<ProductionTimeDataViewModel> Model { get; private set; }
    }
}
