using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions
{
    public interface IDailyReportAggregator
    {
        Task<DimensionReportSummarizedResponseViewModel> FetchDimensionForDay(DateTime date);
        Task<DimensionReportSummarizedResponseViewModel> FetchDimensionForWeek(DateTime date);


        Task<WeeklyReportResponseViewModel> FetchWeeklyReport(DateTime date);


        Task<WeeksReportResponseViewModel> FetchYearlyWeeks(WeeksRequestViewModel model);
    }
}
