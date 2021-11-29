using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.TCO.SharedAPIModels.TCO
{
    public class TCODataAPIResponseViewModel
    {
        public TCODataAPIResponseViewModel() { }

        public string BundleID { get; set; }
        public double ActualTCO { get; set; }
        public double StandardTCO { get; set; }
        public string MaterialNumber { get; set; }
    }
}
