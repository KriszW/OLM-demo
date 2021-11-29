using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.TCO.BackgroundTasks.Updater.Models
{
    public class TCODataSourceModel
    {
        public int? ID { get; set; }
        public double Volume { get; set; }
        public double Primary { get; set; }
        public double Secondary { get; set; }
        public string RawMaterialItemNumber { get; set; }
        public string BundleID { get; set; }
        public string VendorID { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
