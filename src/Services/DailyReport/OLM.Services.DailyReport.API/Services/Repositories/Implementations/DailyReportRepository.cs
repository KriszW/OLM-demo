using Microsoft.EntityFrameworkCore;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Repositories.Implementations
{
    public class DailyReportRepository : IDailyReportRepository
    {
        private readonly TimeSpan _endOfDay = new(0, 23, 59, 59);

        private readonly DailyReportDbContext _dbContext;

        public DailyReportRepository(DailyReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IGrouping<string, DailyReportDataModel>>> GroupByDimension(DateTime start, DateTime end)
        {
            start = start.Date;
            end = end.Date.Add(_endOfDay);

            var data = await _dbContext.ReportData.Where(m => m.Date >= start && m.Date <= end).ToListAsync();

            return data.GroupBy(m => m.Dimension);
        }

        public Task<IEnumerable<IGrouping<string, DailyReportDataModel>>> GroupByDimensionForDay(DateTime date) => GroupByDimension(date, date);
    }
}
