using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp
{
    public class DimensionDayWasteFollowUpState
    {
        public DimensionDayWasteFollowUpState(DateTime date) : this(date, true, default, default) { }
        public DimensionDayWasteFollowUpState(DateTime date, DimensionReportSummarizedResponseViewModel data) : this(date, false, default, data) { }
        public DimensionDayWasteFollowUpState(DateTime date, APIError errors) : this(date, false, errors, default) { }

        public DimensionDayWasteFollowUpState(DateTime date, bool isLoading, APIError errors, DimensionReportSummarizedResponseViewModel data)
        {
            Date = date;
            IsLoading = isLoading;
            Errors = errors;
            Data = data;
        }

        public DateTime Date { get; set; }

        public bool IsLoading { get; private set; }

        public APIError Errors { get; private set; }

        public DimensionReportSummarizedResponseViewModel Data { get; set; }
    }
}
