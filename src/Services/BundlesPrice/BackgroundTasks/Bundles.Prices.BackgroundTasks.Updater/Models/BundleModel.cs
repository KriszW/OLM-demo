using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.Bundles.Prices.BackgroundTasks.Updater.Models
{
    public class BundleModel
    {
        public int? ID { get; set; }
        public string BundleID { get; set; }
        public string ItemNumber { get; set; }
        public string VendorID { get; set; }
    }
}
