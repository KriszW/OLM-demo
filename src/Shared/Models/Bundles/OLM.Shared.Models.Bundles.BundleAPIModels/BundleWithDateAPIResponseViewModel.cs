using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundles.APIResponses
{
    public class BundleWithDateAPIResponseViewModel : BundleAPIResponseViewModel
    {
        public BundleWithDateAPIResponseViewModel() { }
        public DateTime FinishedDate { get; set; }

        public double Primary { get; set; }
        public double Secondary { get; set; }
    }
}
