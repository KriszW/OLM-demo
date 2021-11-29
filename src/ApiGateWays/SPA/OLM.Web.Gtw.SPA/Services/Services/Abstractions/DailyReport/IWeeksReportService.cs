using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport
{
    public interface IWeeksReportService
    {
        Task<WeeksReportResponseViewModel> FetchYearlyWeeks(WeeksRequestViewModel model, IEnumerable<TramResponseViewModel> tramModels);
    }
}
