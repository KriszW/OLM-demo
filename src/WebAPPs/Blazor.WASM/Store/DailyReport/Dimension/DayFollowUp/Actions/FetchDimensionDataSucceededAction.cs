using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Actions
{
    public class FetchDimensionDataSucceededAction
    {
        public FetchDimensionDataSucceededAction(DimensionReportSummarizedResponseViewModel data)
        {
            Data = data;
        }

        public DimensionReportSummarizedResponseViewModel Data { get; set; }
    }
}
