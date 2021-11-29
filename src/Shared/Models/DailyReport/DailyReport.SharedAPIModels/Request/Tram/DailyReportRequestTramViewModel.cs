using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram
{
    public class DailyReportRequestTramViewModel
    {
        public DailyReportRequestTramViewModel() { }

        public DateTime Date { get; set; }
        public string Dimension { get; set; }
        public int NumberOfLammela { get; set; }
        public int NumberOfTram { get; set; }
    }
}
