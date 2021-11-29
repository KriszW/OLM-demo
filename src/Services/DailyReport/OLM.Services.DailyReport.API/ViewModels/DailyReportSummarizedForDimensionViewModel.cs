using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.ViewModels
{
    public class DailyReportSummarizedForDimensionViewModel
    {
        public double TotalM3 { get; set; }
        public double WasteM3 { get; set; }

        public double FSM3 { get; set; }
        public double TramM3 { get; set; }
        public double LammelaM3 { get; set; }

        public double TotalWasteM3 { get; set; }

        public double TargetWasteM3 { get; set; }
    }
}
