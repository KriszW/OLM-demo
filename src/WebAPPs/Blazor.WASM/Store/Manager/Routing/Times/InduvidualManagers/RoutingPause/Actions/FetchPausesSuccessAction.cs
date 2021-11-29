using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions
{
    public class FetchPausesSuccessAction
    {
        public FetchPausesSuccessAction(Paginated<PauseDataViewModel> model)
        {
            Model = model;
        }

        public Paginated<PauseDataViewModel> Model { get; private set; }
    }
}
