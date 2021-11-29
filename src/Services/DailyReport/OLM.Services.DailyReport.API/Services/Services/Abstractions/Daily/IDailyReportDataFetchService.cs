using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions.Daily
{
    public interface IDailyReportDataFetchService
    {
        Task<DimensionReportSummarizedResponseViewModel> FetchDaily(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels);
        Task<DimensionReportSummarizedResponseViewModel> FetchWeekly(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels);
    }
}
