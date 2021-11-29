using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weekly;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using OLM.Services.DailyReport.API.Extensions;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.FileManager
{
    public class WeeklyReportFileWriter : IWeeklyReportFileWriter
    {
        private readonly IWeeklyReportRepository _weeklyReportRepository;
        private readonly ITargetRepository _targetRepository;
        private readonly IDailyReportWorkBookWriter _dailyReportWorkBookWriter;
        private readonly ITargetWorkBookWriter _targetWorkBookWriter;
        private readonly ITramModelWorkBookWriter _tramModelWorkBookWriter;

        public WeeklyReportFileWriter(IWeeklyReportRepository weeklyReportRepository,
                                      ITargetRepository targetRepository,
                                      IDailyReportWorkBookWriter dailyReportWorkBookWriter,
                                      ITargetWorkBookWriter targetWorkBookWriter,
                                      ITramModelWorkBookWriter tramModelWorkBookWriter)
        {
            _weeklyReportRepository = weeklyReportRepository;
            _targetRepository = targetRepository;
            _dailyReportWorkBookWriter = dailyReportWorkBookWriter;
            _targetWorkBookWriter = targetWorkBookWriter;
            _tramModelWorkBookWriter = tramModelWorkBookWriter;
        }

        public async Task<Stream> CreateWeeklyFile(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var dates = GetAllDayForTheWeek(date);

            var dims = tramModels.Select(m => m.Dimension);

            var targetTask = _targetRepository.GetForDimension(dims);

            var data = new List<DailyReportDataModel>();

            foreach (var item in dates)
            {
                var model = await _weeklyReportRepository.GetForDay(item);

                if (model == default) continue;

                data.AddRange(model);
            }

            await Task.WhenAll(targetTask);

            var workBook = new XLWorkbook();

            var dailyReportWriterTask = Task.Run(() => _dailyReportWorkBookWriter.Write(workBook.AddWorksheet("Report adatok"), data.AsEnumerable()));
            var targetWriterTask = Task.Run(() => _targetWorkBookWriter.Write(workBook.AddWorksheet("Target"), targetTask.Result));
            var tramModelWriter = Task.Run(() => _tramModelWorkBookWriter.Write(workBook.AddWorksheet("Csille adatok"), tramModels));

            await Task.WhenAll(dailyReportWriterTask, targetWriterTask, tramModelWriter);

            if (dailyReportWriterTask.Result != default && targetWriterTask.Result != default && tramModelWriter.Result != default) return workBook.SaveWorkBookToStream();
            return default;
        }

        private IEnumerable<DateTime> GetAllDayForTheWeek(DateTime date)
        {
            var currentDayOfWeek = (int)date.DayOfWeek;
            var sunday = date.AddDays(-currentDayOfWeek);
            var monday = sunday.AddDays(1);

            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }

            return Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
        }
    }
}
