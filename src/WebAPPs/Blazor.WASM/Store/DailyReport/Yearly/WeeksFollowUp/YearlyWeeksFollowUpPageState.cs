using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp
{
    public class YearlyWeeksFollowUpPageState
    {
        public YearlyWeeksFollowUpPageState(DateTime start, DateTime end) : this(start, end, true, default, default) { }
        public YearlyWeeksFollowUpPageState(DateTime start, DateTime end, WeeksReportResponseViewModel data) : this(start, end, false, default, data) { }
        public YearlyWeeksFollowUpPageState(DateTime start, DateTime end, APIError errors) : this(start, end, false, errors, default) { }

        public YearlyWeeksFollowUpPageState(DateTime start, DateTime end, bool isLoading, APIError errors, WeeksReportResponseViewModel data)
        {
            Start = start;
            End = end;
            IsLoading = isLoading;
            Errors = errors;
            Data = data;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool IsLoading { get; private set; }

        public APIError Errors { get; private set; }

        public WeeksReportResponseViewModel Data { get; set; }
    }
}
