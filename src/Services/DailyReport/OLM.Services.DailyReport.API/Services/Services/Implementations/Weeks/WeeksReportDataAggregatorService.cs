using OLM.Services.DailyReport.API.Extensions;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weeks;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.Weeks
{
    public class WeeksReportDataAggregatorService : IWeeksReportDataAggregatorService
    {
        private readonly ITargetRepository _targetRepository;
        private readonly IWeeksReportRepository _weeksReportRepository;
        private readonly IDailyReportDataAggregationProviderService _dailyReportDataAggregationProviderService;

        public WeeksReportDataAggregatorService(ITargetRepository targetRepository,
                                                IWeeksReportRepository weeksReportRepository,
                                                IDailyReportDataAggregationProviderService dailyReportDataAggregationProviderService)
        {
            _targetRepository = targetRepository;
            _weeksReportRepository = weeksReportRepository;
            _dailyReportDataAggregationProviderService = dailyReportDataAggregationProviderService;
        }

        public async Task<WeeksReportDataResponseViewModel> Aggregate(YearWeekStartEndViewModel model, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var data = await GetDailySummarizedModels(model, tramModels);

            if (data == default || data.Any() == false) return default;

            var summedM3 = data.Sum(m => m.TotalM3);
            var summedWasteM3 = data.Sum(m => m.WasteM3);
            var summedFSM3 = data.Sum(m => m.FSM3);
            var summedTramM3 = data.Sum(m => m.TramM3);
            var summedLamellaM3 = data.Sum(m => m.LammelaM3);
            var summedTotalWasteM3 = data.Sum(m => m.TotalWasteM3);
            var summedTargetM3 = data.Sum(m => m.TargetWasteM3);

            return new WeeksReportDataResponseViewModel(model.FirstDay, model.LastDay, model.WeekNumber, model.Year) 
            {
                TotalExcludedPlankPercent = 0.0,
                TotalSawPercent = (summedWasteM3 / summedM3).Normalize(),
                TotalFSPercent = (summedFSM3 / summedM3).Normalize(),
                TotalLamellaPercent = (summedLamellaM3 / summedM3).Normalize(),
                TotalTramPercent = (summedTramM3 / summedM3).Normalize(),
                TotalWastePercent = (summedTotalWasteM3 / summedM3).Normalize(),
                TotalTargetPercent = (summedTargetM3 / summedM3).Normalize()
            };
        }

        private async Task<List<DailyReportSummarizedForDimensionViewModel>> GetDailySummarizedModels(YearWeekStartEndViewModel dateModel, IEnumerable<DailyReportRequestTramViewModel> models)
        {
            var tramModels = models.Where(m => m.Date.Date >= dateModel.FirstDay.Date && m.Date.Date <= dateModel.LastDay.Date);

            var dims = tramModels.Select(m => m.Dimension).Distinct();

            var targets = await _targetRepository.GetForDimension(dims);

            var dailySummarizedData = new List<DailyReportSummarizedForDimensionViewModel>();

            foreach (var dim in dims)
            {
                var dimTarget = targets.FirstOrDefault(m => m.Dimension == dim);
                var dimTramModels = models.Where(m => m.Dimension == dim && m.Date.Date >= dateModel.FirstDay.Date && m.Date.Date <= dateModel.LastDay.Date);

                if (dimTramModels == default || dimTramModels.Any() == false) continue;

                var tramModel = new DailyReportRequestTramViewModel
                {
                    Date = dateModel.FirstDay,
                    Dimension = dim,
                    NumberOfLammela = dimTramModels.Sum(m => m.NumberOfLammela),
                    NumberOfTram = dimTramModels.Sum(m => m.NumberOfTram),
                };

                var dimReportData = await _weeksReportRepository.GetDataFor(dateModel);

                if (dimReportData == default || dimTarget == default || dimReportData == default) continue;

                var model = new ModelAggregatorViewModel { Data = dimReportData, TargetModel = dimTarget, TramModel = tramModel };
                var data = await _dailyReportDataAggregationProviderService.AggregateForDimension(model);
                dailySummarizedData.Add(data);
            }

            return dailySummarizedData;
        }
    }
}
