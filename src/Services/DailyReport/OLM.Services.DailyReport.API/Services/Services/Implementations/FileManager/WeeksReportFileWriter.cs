using ClosedXML.Excel;
using OLM.Services.DailyReport.API.Extensions;
using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.FileManager
{
    public class WeeksReportFileWriter : IWeeksReportFileWriter
    {
        private readonly IWeeksReportRepository _weeksReportRepository;
        private readonly ITargetRepository _targetRepository;
        private readonly IDailyReportWorkBookWriter _dailyReportWorkBookWriter;
        private readonly ITargetWorkBookWriter _targetWorkBookWriter;
        private readonly ITramModelWorkBookWriter _tramModelWorkBookWriter;

        public WeeksReportFileWriter(IWeeksReportRepository weeksReportRepository,
                                     ITargetRepository targetRepository,
                                     IDailyReportWorkBookWriter dailyReportWorkBookWriter,
                                     ITargetWorkBookWriter targetWorkBookWriter,
                                     ITramModelWorkBookWriter tramModelWorkBookWriter)
        {
            _weeksReportRepository = weeksReportRepository;
            _targetRepository = targetRepository;
            _dailyReportWorkBookWriter = dailyReportWorkBookWriter;
            _targetWorkBookWriter = targetWorkBookWriter;
            _tramModelWorkBookWriter = tramModelWorkBookWriter;
        }

        public async Task<Stream> CreateWeeksFile(WeeksRequestViewModel model, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var dates = GetWeeks(model);

            var data = new List<DailyReportDataModel>();

            var dims = tramModels.Select(m => m.Dimension);

            var targetTask = _targetRepository.GetForDimension(dims);

            foreach (var date in dates)
            {
                var models = await _weeksReportRepository.GetDataFor(date);

                if (models == default) continue;

                data.AddRange(models);
            }

            await Task.WhenAll(targetTask);

            var workBook = new XLWorkbook();

            var dailyReportWriterTask = Task.Run(() => _dailyReportWorkBookWriter.Write(workBook.AddWorksheet("Report adatok"), data.AsEnumerable()));
            var targetWriterTask = Task.Run(() => _targetWorkBookWriter.Write(workBook.AddWorksheet("Target"), targetTask.Result));
            var tramModelWriter = Task.Run(() => _tramModelWorkBookWriter.Write(workBook.AddWorksheet("Csille adatok"), tramModels));

            await Task.WhenAll(dailyReportWriterTask, targetWriterTask, tramModelWriter);

            if (dailyReportWriterTask.Result != default && targetWriterTask.Result != default && tramModelWriter.Result != default) return workBook.SaveWorkBookToStream();
            else return default;
        }

        private IEnumerable<YearWeekStartEndViewModel> GetWeeks(WeeksRequestViewModel model)
        {
            var output = new List<YearWeekStartEndViewModel>();

            var thisMonday = GetWeeksMondayFromDate(model.Start);
            var thisSunday = GetWeeksSundayFromDate(model.Start);

            while (thisSunday <= model.End)
            {
                output.Add(new YearWeekStartEndViewModel(thisMonday, thisSunday, GetWeekNumber(thisMonday), GetYear(thisMonday)));
                thisMonday = thisMonday.AddDays(7);
                thisSunday = thisSunday.AddDays(7);
            }

            output.Add(new YearWeekStartEndViewModel(thisMonday, thisSunday, GetWeekNumber(thisMonday), GetYear(thisMonday)));

            return output;
        }

        private int GetWeekNumber(DateTime date)
            => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        private int GetYear(DateTime date)
            => date.Year;

        private DateTime GetWeeksMondayFromDate(DateTime date)
        {
            var output = date;

            while (output.DayOfWeek != DayOfWeek.Monday)
            {
                output = output.AddDays(-1);
            }

            return output;
        }

        private DateTime GetWeeksSundayFromDate(DateTime date)
        {
            var output = date;

            while (output.DayOfWeek != DayOfWeek.Sunday)
            {
                output = output.AddDays(1);
            }

            return output;
        }
    }
}
