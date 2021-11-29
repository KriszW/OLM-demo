using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp
{
    public class WeeklyDayFollowUpState
    {
        public WeeklyDayFollowUpState(DateTime date) : this(date, true, default, default) { }
        public WeeklyDayFollowUpState(DateTime date, WeeklyReportResponseViewModel data) : this(date,false, default, data) { }
        public WeeklyDayFollowUpState(DateTime date, APIError errors) : this(date, false, errors, default) { }

        public WeeklyDayFollowUpState(DateTime date, bool isLoading, APIError errors, WeeklyReportResponseViewModel data)
        {
            Date = date;
            IsLoading = isLoading;
            Errors = errors;
            Data = data;
        }

        public DateTime Date { get; set; }

        public bool IsLoading { get; private set; }

        public APIError Errors { get; private set; }

        public WeeklyReportResponseViewModel Data { get; set; }
    }
}
