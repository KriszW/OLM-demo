using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Models
{
    public class BundleModel
    {
        public string Dimension { get; set; }

        public DateTime FinishedDate { get; set; }

        public string MachineName { get; set; }
    }
}
