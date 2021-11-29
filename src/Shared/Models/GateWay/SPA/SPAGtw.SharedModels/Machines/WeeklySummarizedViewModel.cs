using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines
{
    public class WeeklySummarizedViewModel
    {
        public WeeklySummarizedViewModel() { }
        public TCODataViewModel TCO { get; set; }

        public double AllInput { get; set; }
        public double AllGoodProduced { get; set; }
        public double AllFS { get; set; }

        public double WastePercentage { get; set; }
    }
}
