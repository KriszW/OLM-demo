using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report
{
    public class WeeksRequestViewModel
    {
        public WeeksRequestViewModel() { }

        public WeeksRequestViewModel(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
