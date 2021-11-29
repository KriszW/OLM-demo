using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Actions
{
    public class FetchWeeklyDayFollowUpSucceededAction
    {
        public FetchWeeklyDayFollowUpSucceededAction(WeeklyReportResponseViewModel data)
        {
            Data = data;
        }

        public WeeklyReportResponseViewModel Data { get; set; }
    }
}
