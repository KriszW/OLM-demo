using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Actions
{
    public class StartFetchingDimensionDailyWasteFollowUpAction
    {
        public StartFetchingDimensionDailyWasteFollowUpAction(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }
}
