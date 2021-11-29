using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions
{
    public class PauseModelForDeletingSetAction
    {
        public PauseModelForDeletingSetAction(PauseDataViewModel model)
        {
            Model = model;
        }

        public PauseDataViewModel Model { get; set; }
    }
}
