using OLM.Services.DailyReport.API.Models;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions.Weekly
{
    public interface IWeeklyReportDataAggregatorService
    {
        Task<WeeklyReportDataResponseViewModel> AggregateForDay(DateTime date, IEnumerable<DailyReportRequestTramViewModel> models);
    }
}
