using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundle.Prices.APIResponses
{
    public class BundlePriceFromItemNumberViewModel
    {
        public BundlePriceFromItemNumberViewModel() { }
        public string RawMaterialItemNumber { get; set; }
        public string VendorID { get; set; }
    }
}
