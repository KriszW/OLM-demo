using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Actions
{
    public class StartFetchingFollowUpData
    {
        public StartFetchingFollowUpData(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }
}
