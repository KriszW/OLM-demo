using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions
{
    public class SelectedPauseModelChangedAction
    {
        public SelectedPauseModelChangedAction(PauseDataViewModel newSelectedModel)
        {
            NewSelectedModel = newSelectedModel;
        }

        public PauseDataViewModel NewSelectedModel { get; private set; }
    }
}
