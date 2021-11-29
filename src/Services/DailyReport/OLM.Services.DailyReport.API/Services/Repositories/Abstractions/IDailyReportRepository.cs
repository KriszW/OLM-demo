using OLM.Services.DailyReport.API.Models;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Repositories.Abstractions
{
    public interface IDailyReportRepository
    {
        Task<IEnumerable<IGrouping<string,DailyReportDataModel>>> GroupByDimensionForDay(DateTime date);
        Task<IEnumerable<IGrouping<string,DailyReportDataModel>>> GroupByDimension(DateTime start, DateTime end);
    }
}
