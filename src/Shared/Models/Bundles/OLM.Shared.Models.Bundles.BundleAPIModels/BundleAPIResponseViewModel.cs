using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundles.APIResponses
{
    public class BundleAPIResponseViewModel
    {
        public BundleAPIResponseViewModel() { }

        public BundleAPIResponseViewModel(string bundleID,
                                          double input,
                                          double produced,
                                          double fS,
                                          double wastePercentage,
                                          string dimension,
                                          string quality,
                                          string vendorName,
                                          string sawmillName,
                                          string machineName,
                                          DateTime finished)
        {
            BundleID = bundleID;
            Input = input;
            Produced = produced;
            FS = fS;
            WastePercentage = wastePercentage;
            Dimension = dimension;
            Quality = quality;
            VendorName = vendorName;
            SawmillName = sawmillName;
            MachineName = machineName;
            FinishedDate = finished;
        }

        public string BundleID { get; set; }
        public double Input { get; set; }
        public double Produced { get; set; }
        public double FS { get; set; }

        public double WastePercentage { get; set; }

        public string Dimension { get; set; }

        public string Quality { get; set; }

        public string VendorName { get; set; }

        public string SawmillName { get; set; }

        public string MachineName { get; set; }

        public DateTime FinishedDate { get; set; }
    }
}
