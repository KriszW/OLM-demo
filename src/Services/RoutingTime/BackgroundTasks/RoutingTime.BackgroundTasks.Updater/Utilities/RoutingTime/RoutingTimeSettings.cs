using OLM.Services.RoutingTime.BackgroundTasks.Updater.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Utilities.RoutingTime
{
    public class RoutingTimeSettings
    {
        public IEnumerable<PauseTimeSettingsViewModel> Pauses { get; set; }

        public IEnumerable<ProductionTimeSettingsViewModel> ProdTimes { get; set; }
    }
}
