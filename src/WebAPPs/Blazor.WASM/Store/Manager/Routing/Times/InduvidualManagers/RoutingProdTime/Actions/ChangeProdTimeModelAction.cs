using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions
{
    public class ChangeProdTimeModelAction
    {
        public ChangeProdTimeModelAction(ProductionTimeDataViewModel newModelForEdit)
        {
            NewModelForEdit = newModelForEdit;
        }

        public ProductionTimeDataViewModel NewModelForEdit { get; set; }
    }
}
