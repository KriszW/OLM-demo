using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions
{
    public class ChangePauseModelAction
    {
        public ChangePauseModelAction(PauseDataViewModel newModelForEdit)
        {
            NewModelForEdit = newModelForEdit;
        }

        public PauseDataViewModel NewModelForEdit { get; set; }
    }
}
