using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions
{
    public class ProdTimeModelForDeletingSetAction
    {
        public ProdTimeModelForDeletingSetAction(ProductionTimeDataViewModel model)
        {
            Model = model;
        }

        public ProductionTimeDataViewModel Model { get; set; }
    }
}
