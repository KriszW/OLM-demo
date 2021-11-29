using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report
{
    public class WeeksReportDataResponseViewModel
    {
        public WeeksReportDataResponseViewModel() { }
        public WeeksReportDataResponseViewModel(DateTime start,
                                                DateTime end,
                                                int weekNumber,
                                                int year)
        {
            Start = start;
            End = end;
            WeekNumber = weekNumber;
            Year = year;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int WeekNumber { get; set; }
        public int Year { get; set; }


        public double TotalSawPercent { get; set; }
        public double TotalFSPercent { get; set; }
        public double TotalLamellaPercent { get; set; }
        public double TotalTramPercent { get; set; }
        public double TotalExcludedPlankPercent { get; set; }

        public double TotalWastePercent { get; set; }

        public double TotalTargetPercent { get; set; }
    }
}
