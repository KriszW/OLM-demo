using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundles.APIResponses.OneMachine
{
    public class BundleDailyAPIResponseViewModel
    {
        public BundleDailyAPIResponseViewModel(IEnumerable<BundleAPIResponseViewModel> bundles)
        {
            Bundles = bundles;
        }

        public IEnumerable<BundleAPIResponseViewModel> Bundles { get; set; }
    }
}
