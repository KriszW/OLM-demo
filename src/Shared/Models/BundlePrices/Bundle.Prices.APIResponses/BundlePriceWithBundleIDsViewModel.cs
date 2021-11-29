using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundle.Prices.APIResponses
{
    public class BundlePriceWithBundleIDsViewModel : BundlePriceViewModel
    {
        public BundlePriceWithBundleIDsViewModel(string itemNumber, string vendorID, decimal price, IList<string> bundleIDs) : base(itemNumber, vendorID, price)
        {
            BundleIDs = bundleIDs;
        }

        public IList<string> BundleIDs { get; set; }
    }
}
