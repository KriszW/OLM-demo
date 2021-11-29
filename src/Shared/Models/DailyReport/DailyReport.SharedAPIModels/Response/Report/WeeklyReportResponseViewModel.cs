using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report
{
    public class WeeklyReportResponseViewModel
    {
        public WeeklyReportResponseViewModel() { }

        public int WeekOfYear { get; set; }

        public IEnumerable<WeeklyReportDataResponseViewModel> Models { get; set; }
    }
}
