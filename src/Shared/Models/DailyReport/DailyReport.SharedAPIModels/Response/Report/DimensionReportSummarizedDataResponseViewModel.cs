using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report
{
    public class DimensionReportSummarizedDataResponseViewModel
    {
        public DimensionReportSummarizedDataResponseViewModel() { }

        public string Dimension { get; set; }

        public double SawPercent { get; set; }
        public double FSPercent { get; set; }
        public double LamellaPercent { get; set; }
        public double TramPercent { get; set; }
        public double ExcludedPlankPercent { get; set; }


        public double Target { get; set; }
    }
}
