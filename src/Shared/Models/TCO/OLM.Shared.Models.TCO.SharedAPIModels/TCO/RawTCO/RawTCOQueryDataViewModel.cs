using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO
{
    public class RawTCOQueryDataViewModel
    {
        public string ItemNumber { get; set; }

        public string VendorID { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<string> BundleIDs { get; set; }
    }
}
