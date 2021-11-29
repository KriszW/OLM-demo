using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport
{
    public interface IWeeklyReportService
    {
        Task<WeeklyReportResponseViewModel> FetchWeeklyReport(DateTime date, IEnumerable<TramResponseViewModel> tramModels);
    }
}
