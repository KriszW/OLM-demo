using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.RoutingData.BackgroundTasks.Updater.Models
{
    public class RoutingDataModel
    {
        public string Dimension { get; set; }

        public string BundleID { get; set; }

        public double AllLength { get; set; }

        public DateTime FinishedDate { get; set; }

        public string MachineName { get; set; }
    }
}
