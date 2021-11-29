using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Shared.Models.Bundles.APIResponses.SummarizedData
{
    public class SummarizedDataForMachinesViewModel
    {
        [JsonConstructor]
        public SummarizedDataForMachinesViewModel(double allInput, double allProduced, double allFS, double aVGWastePercentage, IEnumerable<string> bundleIDs)
        {
            AllInput = allInput;
            AllProduced = allProduced;
            AllFS = allFS;
            AVGWastePercentage = aVGWastePercentage;
            BundleIDs = bundleIDs;
        }

        public double AllInput { get; set; }
        public double AllProduced { get; set; }
        public double AllFS { get; set; }

        public double AVGWastePercentage { get; set; }

        public IEnumerable<string> BundleIDs { get; set; }
    }
}
