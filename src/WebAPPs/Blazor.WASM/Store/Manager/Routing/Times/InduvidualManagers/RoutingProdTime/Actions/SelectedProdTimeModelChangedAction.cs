using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions
{
    public class SelectedProdTimeModelChangedAction
    {
        public SelectedProdTimeModelChangedAction(ProductionTimeDataViewModel newSelectedModel)
        {
            NewSelectedModel = newSelectedModel;
        }

        public ProductionTimeDataViewModel NewSelectedModel { get; private set; }
    }
}
