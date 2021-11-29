using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common;
using OLM.Services.DailyReport.API.Extensions;
using ClosedXML.Excel;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.FileManager
{
    public class DailyReportFileWriter : IDailyReportFileWriter
    {
        private readonly IDailyReportRepository _dailyReportRepository;
        private readonly ITargetRepository _targetRepository;
        private readonly IDailyReportWorkBookWriter _dailyReportWorkBookWriter;
        private readonly ITargetWorkBookWriter _targetWorkBookWriter;
        private readonly ITramModelWorkBookWriter _tramModelWorkBookWriter;

        public DailyReportFileWriter(IDailyReportRepository dailyReportRepository,
                                     ITargetRepository targetRepository,
                                     IDailyReportWorkBookWriter dailyReportWorkBookWriter,
                                     ITargetWorkBookWriter targetWorkBookWriter,
                                     ITramModelWorkBookWriter tramModelWorkBookWriter)
        {
            _dailyReportRepository = dailyReportRepository;
            _targetRepository = targetRepository;
            _dailyReportWorkBookWriter = dailyReportWorkBookWriter;
            _targetWorkBookWriter = targetWorkBookWriter;
            _tramModelWorkBookWriter = tramModelWorkBookWriter;
        }

        public async Task<Stream> CreateDimensionForDailyFile(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var dims = tramModels.Select(m => m.Dimension);

            var reportDataTask = _dailyReportRepository.GroupByDimensionForDay(date);
            var targetsTask = _targetRepository.GetForDimension(dims);

            await Task.WhenAll(reportDataTask, targetsTask);

            if (reportDataTask.Result == default || targetsTask.Result == default) return default;

            using var workBook = new XLWorkbook();

            var dailyReportWriterTask = Task.Run(() => _dailyReportWorkBookWriter.Write(workBook.AddWorksheet("Report adatok"), reportDataTask.Result));
            var targetWriterTask = Task.Run(() => _targetWorkBookWriter.Write(workBook.AddWorksheet("Target"), targetsTask.Result));
            var tramModelWriter = Task.Run(() => _tramModelWorkBookWriter.Write(workBook.AddWorksheet("Csille adatok"), tramModels));

            await Task.WhenAll(dailyReportWriterTask, targetWriterTask, tramModelWriter);

            if (dailyReportWriterTask.Result != default && targetWriterTask.Result != default && tramModelWriter.Result != default) return workBook.SaveWorkBookToStream();
            else return default;
        }

        public async Task<Stream> CreateDimensionForWeekFile(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var start = date.GetDateOfTheStartOfWeek(DayOfWeek.Monday);
            var end = date.GetDateOfTheEndOfWeek(DayOfWeek.Sunday);

            var dims = tramModels.Select(m => m.Dimension);

            var reportDataTask = _dailyReportRepository.GroupByDimension(start, end);
            var targetsTask = _targetRepository.GetForDimension(dims);

            await Task.WhenAll(reportDataTask, targetsTask);

            if (reportDataTask.Result == default || targetsTask.Result == default) return default;

            using var workBook = new XLWorkbook();

            var dailyReportWriterTask = Task.Run(() => _dailyReportWorkBookWriter.Write(workBook.AddWorksheet("Report adatok"), reportDataTask.Result));
            var targetWriterTask = Task.Run(() => _targetWorkBookWriter.Write(workBook.AddWorksheet("Target"), targetsTask.Result));
            var tramModelWriter = Task.Run(() => _tramModelWorkBookWriter.Write(workBook.AddWorksheet("Csille adatok"), tramModels));

            await Task.WhenAll(dailyReportWriterTask, targetWriterTask, tramModelWriter);

            if (dailyReportWriterTask.Result != default && targetWriterTask.Result != default && tramModelWriter.Result != default) return workBook.SaveWorkBookToStream();
            else return default;
        }
    }
}
