using OLM.Services.DailyReport.API.Extensions;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weekly;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.Weekly
{
    public class WeeklyReportDataAggregatorService : IWeeklyReportDataAggregatorService
    {
        private readonly IWeeklyReportRepository _weeklyReportRepository;
        private readonly ITargetRepository _targetRepository;
        private readonly IDailyReportDataAggregationProviderService _dailyReportDataAggregationProviderService;

        public WeeklyReportDataAggregatorService(IWeeklyReportRepository weeklyReportRepository,
                                                 ITargetRepository targetRepository,
                                                 IDailyReportDataAggregationProviderService dailyReportDataAggregationProviderService)
        {
            _weeklyReportRepository = weeklyReportRepository;
            _targetRepository = targetRepository;
            _dailyReportDataAggregationProviderService = dailyReportDataAggregationProviderService;
        }

        public async Task<WeeklyReportDataResponseViewModel> AggregateForDay(DateTime date, IEnumerable<DailyReportRequestTramViewModel> models)
        {
            var data = await GetDailySummarizedModels(date, models);

            if (data == default || data.Any() == false) return default;

            var summedM3 = data.Sum(m => m.TotalM3);
            var summedWasteM3 = data.Sum(m => m.WasteM3);
            var summedFSM3 = data.Sum(m => m.FSM3);
            var summedTramM3 = data.Sum(m => m.TramM3);
            var summedLamellaM3 = data.Sum(m => m.LammelaM3);
            var summedTotalWasteM3 = data.Sum(m => m.TotalWasteM3);
            var summedTargetM3 = data.Sum(m => m.TargetWasteM3);

            return new WeeklyReportDataResponseViewModel
            {
                Date = date,
                TotalExcludedPlankPercent = 0.0,
                TotalSawPercent = (summedWasteM3 / summedM3).Normalize(),
                TotalFSPercent = (summedFSM3 / summedM3).Normalize(),
                TotalLamellaPercent = (summedLamellaM3 / summedM3).Normalize(),
                TotalTramPercent = (summedTramM3 / summedM3).Normalize(),
                TotalWastePercent = (summedTotalWasteM3 / summedM3).Normalize(),
                TotalTargetPercent = (summedTargetM3 / summedM3).Normalize()
            };
        }

        private async Task<List<DailyReportSummarizedForDimensionViewModel>> GetDailySummarizedModels(DateTime date, IEnumerable<DailyReportRequestTramViewModel> models)
        {
            var tramModels = models.Where(m => m.Date.Date == date.Date);

            var dims = tramModels.Select(m => m.Dimension).Distinct();

            var targets = await _targetRepository.GetForDimension(dims);

            var dailySummarizedData = new List<DailyReportSummarizedForDimensionViewModel>();

            foreach (var dim in dims)
            {
                var dimTarget = targets.FirstOrDefault(m => m.Dimension == dim);

                var dimTramModels = tramModels.Where(m => m.Dimension == dim && m.Date.Date == date.Date);

                if (dimTramModels == default || dimTramModels.Any() == false) continue;

                var tramModel = new DailyReportRequestTramViewModel
                {
                    Date = date,
                    Dimension = dim,
                    NumberOfLammela = dimTramModels.Sum(m => m.NumberOfLammela),
                    NumberOfTram = dimTramModels.Sum(m => m.NumberOfTram),
                };

                var dimReportData = await _weeklyReportRepository.GetForDayAndDimension(date, dim);

                if (dimReportData == default || dimTarget == default || dimReportData == default) continue;

                var model = new ModelAggregatorViewModel { Data = dimReportData, TargetModel = dimTarget, TramModel = tramModel };
                var data = await _dailyReportDataAggregationProviderService.AggregateForDimension(model);
                dailySummarizedData.Add(data);
            }

            return dailySummarizedData;
        }
    }
}
