using OLM.Shared.Models.Bundles.APIResponses;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Bundle
{
    public class TCOBundleModel
    {
        public TCOBundleModel() { }
        public TCOBundleModel(BundleWithDateAPIResponseViewModel bundleModel, TCODataAPIResponseViewModel tcoModel)
        {
            BundleID = bundleModel.BundleID;
            Input = bundleModel.Input;
            Good = bundleModel.Primary + bundleModel.Secondary + bundleModel.FS;
            FS = bundleModel.FS;

            GoodRate = bundleModel.Primary / (bundleModel.Primary + bundleModel.Secondary);
            if (double.IsNaN(GoodRate)) GoodRate = 0;

            StandardTCO = tcoModel.StandardTCO;
            ActualTCO = tcoModel.ActualTCO;
            Dimension = bundleModel.Dimension;
            MaterialNumber = tcoModel.MaterialNumber;
            Quality = bundleModel.Quality;
            Vendor = bundleModel.VendorName;
            Sawmill = bundleModel.SawmillName;
            FinishedDate = bundleModel.FinishedDate;
        }

        public int? ID { get; set; }
        public string BundleID { get; set; }
        public double Input { get; set; }
        public double Good { get; set; }
        public double FS { get; set; }
        public double GoodRate { get; set; }
        public double StandardTCO { get; set; }
        public double ActualTCO { get; set; }
        public string Dimension { get; set; }
        public string MaterialNumber { get; set; }
        public string Quality { get; set; }
        public string Vendor { get; set; }
        public string Sawmill { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
