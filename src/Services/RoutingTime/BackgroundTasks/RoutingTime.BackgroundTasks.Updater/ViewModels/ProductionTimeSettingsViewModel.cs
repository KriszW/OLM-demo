using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.ViewModels
{
    public class ProductionTimeSettingsViewModel
    {
        public TimeSpan StartOffset { get; set; }

        public TimeSpan EndOffSet { get; set; }

        public DayOfWeek Day { get; set; }

        public string MachineName { get; set; }
    }
}
