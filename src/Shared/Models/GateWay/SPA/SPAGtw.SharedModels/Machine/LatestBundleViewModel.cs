using OLM.Shared.Models.Bundles.APIResponses;
using OLM.Shared.Models.CategoryBulbs.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine
{
    public class LatestBundleViewModel
    {
        public LatestBundleViewModel() { }
        public BundleAPIResponseViewModel Bundle { get; set; }

        public TCODataViewModel TCO { get; set; }

        public IEnumerable<ValidationResult> CategoryBulbs { get; set; }
    }
}
