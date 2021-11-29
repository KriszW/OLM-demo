using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.TCO.SharedAPIModels.TCO.TCOSettings
{
    public class TCOSettingsViewModel
    {
        public TCOSettingsViewModel() { }

        public int? ID { get; set; }
        public string RawMaterialItemNumber { get; set; }
        public double MaximumDifference { get; set; }
        public double ExpectedTCOValue { get; set; }
    }
}
