using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OLM.Services.DailyReport.API.Extensions;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Daily;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.Daily
{
    public class DailyReportDataFetchService : IDailyReportDataFetchService
    {
        private readonly IDailyReportRepository _dailyReportRepository;
        private readonly ITargetRepository _targetRepository;
        private readonly IDailyReportDataAggregationProviderService _dailyReportDataAggregationProviderService;

        public DailyReportDataFetchService(IDailyReportRepository dailyReportRepository,
                                           ITargetRepository targetRepository,
                                           IDailyReportDataAggregationProviderService dailyReportDataAggregationProviderService)
        {
            _dailyReportRepository = dailyReportRepository;
            _targetRepository = targetRepository;
            _dailyReportDataAggregationProviderService = dailyReportDataAggregationProviderService;
        }

        public async Task<DimensionReportSummarizedResponseViewModel> FetchDaily(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var data = await _dailyReportRepository.GroupByDimensionForDay(date);

            if (data == default || data.Any() == false) return default;

            var dims = data.Select(m => m.Key);

            var models = new List<DimensionReportSummarizedDataResponseViewModel>();

            foreach (var dim in dims)
            {
                var dataObjects = data.FirstOrDefault(m => m.Key == dim);

                var dimTramModels = tramModels.Where(m => m.Dimension == dim);

                if (dimTramModels == default || dimTramModels.Any() == false) continue;

                var tramModel = new DailyReportRequestTramViewModel
                {
                    Date = date,
                    Dimension = dim,
                    NumberOfLammela = dimTramModels.Sum(m => m.NumberOfLammela),
                    NumberOfTram = dimTramModels.Sum(m => m.NumberOfTram),
                };

                var target = await _targetRepository.GetForDimension(dim);

                if (target == default
                    || tramModel == default
                    || dataObjects == default
                    || dataObjects.Any() == false) continue;

                var model = new ModelAggregatorViewModel { Data = dataObjects, TargetModel = target, TramModel = tramModel };
                var sumModel = await _dailyReportDataAggregationProviderService.AggregateForDimension(model);

                if (sumModel == default) continue;

                models.Add(sumModel.Summarize(target));
            }

            return new DimensionReportSummarizedResponseViewModel
            {
                Date = date,
                DimensionReportData = models,
            };
        }

        public async Task<DimensionReportSummarizedResponseViewModel> FetchWeekly(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var start = date.GetDateOfTheStartOfWeek(DayOfWeek.Monday);
            var end = date.GetDateOfTheEndOfWeek(DayOfWeek.Sunday);

            var data = await _dailyReportRepository.GroupByDimension(start, end);

            if (data == default || data.Any() == false) return default;

            var dims = data.Select(m => m.Key);

            var models = new List<DimensionReportSummarizedDataResponseViewModel>();

            foreach (var dim in dims)
            {
                var dataObjects = data.FirstOrDefault(m => m.Key == dim);

                var dimTramModels = tramModels.Where(m => m.Dimension == dim);

                if (dimTramModels == default || dimTramModels.Any() == false) continue;

                var tramModel = new DailyReportRequestTramViewModel 
                { 
                    Date = date, 
                    Dimension = dim, 
                    NumberOfLammela = dimTramModels.Sum(m => m.NumberOfLammela), 
                    NumberOfTram = dimTramModels.Sum(m => m.NumberOfTram), 
                };

                var target = await _targetRepository.GetForDimension(dim);

                if (target == default
                    || tramModel == default
                    || dataObjects == default
                    || dataObjects.Any() == false) continue;

                var model = new ModelAggregatorViewModel { Data = dataObjects, TargetModel = target, TramModel = tramModel };
                var sumModel = await _dailyReportDataAggregationProviderService.AggregateForDimension(model);

                if (sumModel == default) continue;

                models.Add(sumModel.Summarize(target));
            }

            return new DimensionReportSummarizedResponseViewModel
            {
                Date = date,
                DimensionReportData = models,
            };
        }

    }
}
