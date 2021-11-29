using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weeks;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.Weeks
{
    public class WeeksReportFetchService : IWeeksReportFetchService
    {
        private readonly IWeeksReportDataAggregatorService _weeksReportDataAggregatorService;

        public WeeksReportFetchService(IWeeksReportDataAggregatorService weeksReportDataAggregatorService)
        {
            _weeksReportDataAggregatorService = weeksReportDataAggregatorService;
        }

        public async Task<WeeksReportResponseViewModel> Fetch(WeeksRequestViewModel model, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var dates = GetWeeks(model);

            var outputData = new List<WeeksReportDataResponseViewModel>();

            foreach (var date in dates)
            {
                var data = await _weeksReportDataAggregatorService.Aggregate(date, tramModels);

                if (data == default) continue;

                outputData.Add(data);
            }

            return new WeeksReportResponseViewModel(model.Start, model.End, outputData);
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
