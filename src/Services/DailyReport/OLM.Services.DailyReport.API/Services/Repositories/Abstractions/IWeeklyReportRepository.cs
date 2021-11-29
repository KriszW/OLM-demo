using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Repositories.Abstractions
{
    public interface IWeeklyReportRepository
    {
        Task<List<DailyReportDataModel>> GetForDay(DateTime date);
        Task<List<DailyReportDataModel>> GetForDayAndDimension(DateTime date, string dimension);

        Task<SummarizedDailyReportDataViewModel> GetSummarizedForDay(DateTime date);
    }
}
