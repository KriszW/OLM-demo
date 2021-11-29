using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundle.Prices.APIResponses
{
    public class BundlePriceDTOViewModel
    {
        public BundlePriceDTOViewModel() { }

        public int? ID { get; set; }

        public string RawMaterialItemNumber { get; set; }

        public decimal Price { get; set; }

        public string VendorID { get; set; }

        public string Currency { get; set; }
    }
}
