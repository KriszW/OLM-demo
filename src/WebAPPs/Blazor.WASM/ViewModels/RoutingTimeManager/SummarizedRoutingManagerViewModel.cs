using System;
using System.Collections.Generic;
using System.Globalization;

namespace OLM.Blazor.WASM.ViewModels.RoutingTimeManager
{
    public class SummarizedRoutingManagerViewModel
    {
        public SummarizedRoutingManagerViewModel()
        {
            Days = new List<DayOfWeek>();

            MachineNames = new List<string>();

            WeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public IList<DayOfWeek> Days { get; set; }

        public IList<string> MachineNames { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int WeekNumber { get; set; }
    }
}
