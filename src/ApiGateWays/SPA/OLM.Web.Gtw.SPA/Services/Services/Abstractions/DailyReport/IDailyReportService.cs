using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport
{
    public interface IDailyReportService
    {
        Task<DimensionReportSummarizedResponseViewModel> FechDimensionForDay(DateTime date, IEnumerable<TramResponseViewModel> tramModels);
        Task<DimensionReportSummarizedResponseViewModel> FechDimensionForWeek(DateTime date, IEnumerable<TramResponseViewModel> tramModels);
    }
}
