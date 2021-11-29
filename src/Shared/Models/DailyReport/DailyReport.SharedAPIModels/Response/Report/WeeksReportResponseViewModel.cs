using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report
{
    public class WeeksReportResponseViewModel
    {
        public WeeksReportResponseViewModel() { }
        public WeeksReportResponseViewModel(DateTime start, DateTime end, IEnumerable<WeeksReportDataResponseViewModel> data)
        {
            Start = start;
            End = end;
            Data = data;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IEnumerable<WeeksReportDataResponseViewModel> Data { get; set; }
    }
}
