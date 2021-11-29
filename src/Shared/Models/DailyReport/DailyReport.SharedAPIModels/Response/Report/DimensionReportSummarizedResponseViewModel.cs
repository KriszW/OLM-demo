using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report
{
    public class DimensionReportSummarizedResponseViewModel
    {
        public DimensionReportSummarizedResponseViewModel() { }

        public DateTime Date { get; set; }

        public IEnumerable<DimensionReportSummarizedDataResponseViewModel> DimensionReportData { get; set; }
    }
}
