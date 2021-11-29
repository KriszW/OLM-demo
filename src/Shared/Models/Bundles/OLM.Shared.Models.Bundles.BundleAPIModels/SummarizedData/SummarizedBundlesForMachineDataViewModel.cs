using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundles.APIResponses.SummarizedData
{
    public class SummarizedBundlesForMachineDataViewModel : SummarizedDataForMachinesViewModel
    {
        public SummarizedBundlesForMachineDataViewModel(double allInput, double allProduced, double allFS, double aVGWastePercentage, string machineName, IEnumerable<string> bundleIDs) : base(allInput, allProduced, allFS, aVGWastePercentage, bundleIDs)
        {
            MachineName = machineName;
        }

        public string MachineName { get; set; }
    }
}
