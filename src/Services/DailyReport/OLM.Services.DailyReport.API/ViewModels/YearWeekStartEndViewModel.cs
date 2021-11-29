using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.ViewModels
{
    public class YearWeekStartEndViewModel
    {
        public YearWeekStartEndViewModel(DateTime firstDay, DateTime lastDay, int weekNumber, int year)
        {
            FirstDay = firstDay;
            LastDay = lastDay;
            WeekNumber = weekNumber;
            Year = year;
        }

        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }

        public int WeekNumber { get; set; }

        public int Year { get; set; }
    }
}
