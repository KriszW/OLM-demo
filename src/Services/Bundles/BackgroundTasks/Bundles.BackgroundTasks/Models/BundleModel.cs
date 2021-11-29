using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Models
{
    public class BundleModel
    {
        public int? ID { get; set; }
        public string BundleID { get; set; }
        public double Input { get; set; }
        public double Primary { get; set; }
        public double Secondary { get; set; }
        public double Produced { get; set; }
        public double FS { get; set; }
        public double Waste { get; set; }
        public string Dimension { get; set; }
        public string Quality { get; set; }
        public string VendorName { get; set; }
        public string SawmillName { get; set; }
        public string MachineName { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
