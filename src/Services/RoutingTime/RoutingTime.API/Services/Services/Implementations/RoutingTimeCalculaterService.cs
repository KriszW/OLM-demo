using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.API.Services.Services.Abstractions;
using OLM.Services.RoutingTime.API.Utilities;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Services.Services.Implementations
{
    public class RoutingTimeCalculaterService : IRoutingTimeCalculaterService
    {
        private readonly IProductionTimeRepository _productionTimeRepository;
        private readonly IPausesRepository _pausesRepository;
        private readonly IBundlesRepository _bundlesRepository;

        public RoutingTimeCalculaterService(IProductionTimeRepository productionTimeRepository,
                                            IPausesRepository pausesRepository,
                                            IBundlesRepository bundlesRepository)
        {
            _productionTimeRepository = productionTimeRepository;
            _pausesRepository = pausesRepository;
            _bundlesRepository = bundlesRepository;
        }

        public async Task<List<RoutingTimesDataResponseViewModel>> Calculate(string machineName, DateTime start, DateTime end)
        {
            var bundles = await _bundlesRepository.FetchAll(machineName, start, end);
            var pauses = await _pausesRepository.FetchBetween(machineName, start, end);
            var productionTime = await _productionTimeRepository.FetchBetween(machineName, start, end);

            var output = UploadWithDims(bundles);

            var time = GetStartTime(start, productionTime);

            if (time.HasValue == false) return default;

            var latestFinish = start.Date.Add(time.Value);

            for (int i = 0; i < bundles.Count; i++)
            {
                var bundle = bundles[i];
                var nextBundle = i == bundles.Count - 1 ? default : bundles[i + 1];

                ManageForSameDay(pauses, output, latestFinish, bundle);

                latestFinish = bundle.FinishedDate;

                if (nextBundle != default && bundle.FinishedDate.Date != nextBundle.FinishedDate.Date)
                {
                    AddLeftProductionTimeFromTheDay(pauses, productionTime, output, latestFinish, nextBundle);

                    var dayTime = GetStartOfProdTime(nextBundle.FinishedDate, productionTime);

                    if (dayTime.HasValue == false) continue;

                    latestFinish = nextBundle.FinishedDate.Date.Add(dayTime.Value);
                }
            }

            return SetAllTime(productionTime, output);
        }

        private TimeSpan? GetStartTime(DateTime start, List<ProductionTimeModel> productionTime)
        {
            var time = GetStartOfProdTime(start, productionTime);

            var offset = 1;

            while (time.HasValue == false)
            {
                time = GetStartOfProdTime(start.AddDays(offset), productionTime);

                offset++;
            }

            return time;
        }

        private void AddLeftProductionTimeFromTheDay(List<PauseModel> pauses, List<ProductionTimeModel> productionTime, List<RoutingTimesDataResponseViewModel> output, DateTime latestFinish, BundleModel nextBundle)
        {
            // Ha pl a következő rakat már átcsúszna másnapra akkor hogy egyszerűbb legyen a számolás
            // Kiválasztom a következő rakat adatait és a mostani rakat végétől számított időt ami a napból maradt
            // Hozzáadom és a mostani rakat befejezési idejétől számított szünet számításba véve

            var time = GetEndOfProductionTime(latestFinish, productionTime);

            if (time.HasValue)
            {
                var endOfDay = latestFinish.Date.Add(time.Value);

                var outputData = GetOutputDataByDimension(nextBundle.Dimension, output);

                var leftFromDay = GetNumberOfMinutesToTheEndOfProductionDay(latestFinish, productionTime);
                var pauseTime = GetNumberOfPauseMinutesBetween(latestFinish, endOfDay, pauses);

                outputData.ProductionMinutes += leftFromDay - pauseTime;
                outputData.PauseMinutes += pauseTime;
            }
        }

        private void ManageForSameDay(List<PauseModel> pauses, List<RoutingTimesDataResponseViewModel> output, DateTime latestFinish, BundleModel bundle)
        {
            var currentDim = bundle.Dimension;

            var outputData = GetOutputDataByDimension(currentDim, output);

            var bundleProductionTime = (bundle.FinishedDate - latestFinish).TotalMinutes;
            var pauseTime = GetNumberOfPauseMinutesBetween(latestFinish, bundle.FinishedDate, pauses);

            var totalProdTime = (int)bundleProductionTime - pauseTime;

            outputData.ProductionMinutes += totalProdTime;
            outputData.PauseMinutes += pauseTime;
        }

        private List<RoutingTimesDataResponseViewModel> SetAllTime(List<ProductionTimeModel> productionTime, List<RoutingTimesDataResponseViewModel> output)
        {
            var allTime = productionTime.Sum(m => (int)(m.End - m.Start).TotalMinutes);

            foreach (var item in output)
            {
                item.AllTime = allTime;
            }

            return output;
        }

        private TimeSpan? GetStartOfProdTime(DateTime date, List<ProductionTimeModel> prodTimes)
        {
            var time = prodTimes.FirstOrDefault(m => m.Start.Date == date.Date);

            if (time != default)
                return time.Start.TimeOfDay;
            else
                return null;
        }

        private TimeSpan? GetEndOfProductionTime(DateTime date, List<ProductionTimeModel> prodTimes)
        {
            var time = prodTimes.FirstOrDefault(m => m.Start.Date == date.Date);

            if (time != default)
                return time.End.TimeOfDay;
            else
                return null;
        }

        private int GetNumberOfMinutesToTheEndOfProductionDay(DateTime date, List<ProductionTimeModel> prodTimes)
        {
            var prodTime = prodTimes.FirstOrDefault(m => m.Start.Date == date.Date);

            var endOfDay = prodTime.End;

            return (int)(endOfDay - date).TotalMinutes;
        }

        private int GetNumberOfPauseMinutesBetween(DateTime start, DateTime end, List<PauseModel> pauses)
        {
            var range = new DateTimeRange(start, end);

            var pausesInTime = pauses.Where(m => new DateTimeRange(m.Start, m.End).Intersects(range));

            return pausesInTime.Sum(m => (int)(m.End - m.Start).TotalMinutes);
        }

        private RoutingTimesDataResponseViewModel GetOutputDataByDimension(string dimension, List<RoutingTimesDataResponseViewModel> output)
            => output.First(m => m.Dimension == dimension);

        private List<RoutingTimesDataResponseViewModel> UploadWithDims(List<BundleModel> bundles)
        {
            var output = new List<RoutingTimesDataResponseViewModel>();

            var dims = bundles.Select(m => m.Dimension).Distinct();

            foreach (var dim in dims)
            {
                output.Add(new RoutingTimesDataResponseViewModel { Dimension = dim });
            }

            return output;
        }
    }
}
