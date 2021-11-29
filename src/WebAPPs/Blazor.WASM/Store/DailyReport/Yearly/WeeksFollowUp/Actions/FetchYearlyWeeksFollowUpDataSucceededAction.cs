using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp.Actions
{
    public class FetchYearlyWeeksFollowUpDataSucceededAction
    {
        public FetchYearlyWeeksFollowUpDataSucceededAction(WeeksReportResponseViewModel data)
        {
            Data = data;
        }

        public WeeksReportResponseViewModel Data { get; set; }
    }
}
