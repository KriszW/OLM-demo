using OLM.Shared.Models.Bundles.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundles.APIResponses.Summarized
{
    public class DailySummarizedBundlesAPIResponseViewModel
    {
        public DailySummarizedBundlesAPIResponseViewModel(IEnumerable<BundleAPIResponseViewModel> bundles)
        {
            Bundles = bundles;
        }

        public IEnumerable<BundleAPIResponseViewModel> Bundles { get; set; }
    }
}
