using Microsoft.EntityFrameworkCore;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Repositories.Implementations
{
    public class WeeksReportRepository : IWeeksReportRepository
    {
        private readonly DailyReportDbContext _dbContext;

        public WeeksReportRepository(DailyReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<DailyReportDataModel>> GetDataFor(YearWeekStartEndViewModel model)
            => _dbContext.ReportData.Where(m => m.Date.Date >= model.FirstDay.Date && m.Date.Date <= model.LastDay.Date).ToListAsync();
    }
}
