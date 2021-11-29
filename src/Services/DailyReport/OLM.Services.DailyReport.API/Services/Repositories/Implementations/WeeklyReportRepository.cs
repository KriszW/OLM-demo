using Microsoft.EntityFrameworkCore;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Repositories.Implementations
{
    public class WeeklyReportRepository : IWeeklyReportRepository
    {
        private readonly DailyReportDbContext _dbContext;

        public WeeklyReportRepository(DailyReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<DailyReportDataModel>> GetForDay(DateTime date)
            => _dbContext.ReportData.Where(m => m.Date.Date == date.Date).ToListAsync();

        public Task<List<DailyReportDataModel>> GetForDayAndDimension(DateTime date, string dimension)
            => _dbContext.ReportData.Where(m => m.Date.Date == date.Date && m.Dimension == dimension).ToListAsync();

        public async Task<SummarizedDailyReportDataViewModel> GetSummarizedForDay(DateTime date)
        {
            var data = await GetForDay(date);

            if (data == default || data.Any() == false) throw new APIErrorException($"A {date:G} naphoz nincsen napireport adat");

            var summedLengthTask = Task.Run(() => data.Sum(m=> m.Length) );
            var summedLengthOfWasteTask = Task.Run(() => data.Sum(m=> m.LengthOfWaste) );
            var summedLengthOfFSTask = Task.Run(() => data.Sum(m=> m.LengthOfFS) );

            await Task.WhenAll(summedLengthTask, summedLengthOfWasteTask, summedLengthOfFSTask);

            if (summedLengthTask.IsFaulted == true ||
                summedLengthOfWasteTask.IsFaulted == true ||
                summedLengthOfFSTask.IsFaulted == true)
            {
                throw new AggregateException(summedLengthTask.Exception, summedLengthOfWasteTask.Exception, summedLengthOfFSTask.Exception);
            }
                

            return new SummarizedDailyReportDataViewModel(summedLengthTask.Result, summedLengthOfWasteTask.Result, summedLengthOfFSTask.Result);
        }
    }
}
