using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundle.Prices.APIResponses
{
    public class BundlePriceViewModel
    {
        public BundlePriceViewModel(string itemNumber, string vendorID, decimal price)
        {
            ItemNumber = itemNumber;
            VendorID = vendorID;
            Price = price;
        }

        public string ItemNumber { get; set; }
        public string VendorID { get; set; }
        public decimal Price { get; set; }
    }
}
