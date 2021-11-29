using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice
{
    public class TCOBundlePriceViewModel
    {
        public TCOBundlePriceViewModel() { }

        public int? ID { get; set; }

        public decimal Price { get; set; }

        public string RawMaterialItemNumber { get; set; }
    }
}
