using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report
{
    public class WeeklyReportDataResponseViewModel
    {
        public WeeklyReportDataResponseViewModel() { }

        public DateTime Date { get; set; }

        public double TotalSawPercent { get; set; }
        public double TotalFSPercent { get; set; }
        public double TotalLamellaPercent { get; set; }
        public double TotalTramPercent { get; set; }
        public double TotalExcludedPlankPercent { get; set; }

        public double TotalWastePercent { get; set; }

        public double TotalTargetPercent { get; set; }
    }
}
