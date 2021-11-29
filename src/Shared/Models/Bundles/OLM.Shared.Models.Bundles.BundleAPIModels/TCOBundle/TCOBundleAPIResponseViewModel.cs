using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundles.APIResponses.TCOBundle
{
    public class TCOBundleAPIResponseViewModel
    {
        public string BundleID { get; set; }
        public double Input { get; set; }
        public double Good { get; set; }
        public double FS { get; set; }
        public double GoodRate { get; set; }
        public double StandardTCO { get; set; }
        public double ActualTCO { get; set; }
        public string Dimension { get; set; }
        public string Quality { get; set; }
        public string Vendor { get; set; }
        public string Sawmill { get; set; }
        public string MaterialNumber { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
