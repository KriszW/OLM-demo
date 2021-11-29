using Microsoft.Extensions.Logging;
using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Services.Abstractions;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Bundle;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Services.Implementations
{
    public class TCOUpdaterService : ITCOUpdaterService
    {
        private const int MaxNumberOfDifferenceDays = -60;
        private ITCOSourceBundlesRepository _sourceBundlesRepository => _repoFactory.CreateTCOSourceRepo();
        private ITCODestinationBundlesRepository _destinationBundlesRepository => _repoFactory.CreateTCODestinationRepo();

        public DateTime LatestFinishedDate { get; set; }

        private readonly IRepositoryFactory _repoFactory;
        private readonly ILogger<TCOUpdaterService> _logger;

        public TCOUpdaterService(IRepositoryFactory repoFactory,
                              ILogger<TCOUpdaterService> logger)
        {
            _repoFactory = repoFactory;
            _logger = logger;
        }

        public async Task UpdateFinishedDate()
        {
            try
            {
                LatestFinishedDate = await FetchLatestDate();

                _logger.LogInformation($"A legfrisebb rakat feltöltési dátuma beállítva: {LatestFinishedDate}");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"A legfrisebb rakat feltöltési dátumának lekérése közben hiba lépett fel: {ex.Message}");
            }
        }

        public async Task Update()
        {
            var intervalEnd = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddMinutes(59);

            if ((DateTime.Now - LatestFinishedDate).TotalHours > 24)
            {
                var date = LatestFinishedDate;
                var end = new DateTime(LatestFinishedDate.Year, LatestFinishedDate.Month, LatestFinishedDate.Day, 23, 59, 59);

                while (date < intervalEnd)
                {
                    await UpdateBundles(date, end);

                    date = date.AddDays(1);
                    end = end.AddDays(1);
                }
            }
            else
            {
                var endOfLatestFinishedDateDay = new DateTime(LatestFinishedDate.Year, LatestFinishedDate.Month, LatestFinishedDate.Day, 23, 59, 59);
                await UpdateBundles(LatestFinishedDate.AddSeconds(1), endOfLatestFinishedDateDay.AddDays(1));
            }
        }

        private async Task UpdateBundles(DateTime from, DateTime to)
        {
            var latestBundles = await FetchModelsFromLatestFinishedDate(from, to);

            if (latestBundles?.Count() != 1)
            {
                await UploadNewModels(latestBundles);
            }
            else
            {
                var alreadyUploaded = await _destinationBundlesRepository.AlreadyUploaded(latestBundles.First().BundleID);
                if(alreadyUploaded == false ) await UploadNewModels(latestBundles);
            }
            
        }

        private async Task<IEnumerable<TCOBundleModel>> FetchModelsFromLatestFinishedDate(DateTime from, DateTime to)
        {
            try
            {
                return await _sourceBundlesRepository.QueryBundlesFromDate(from, to);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"A rakatok lekérdezés {LatestFinishedDate}-től query közben hiba lépett fel: {ex.Message}");
            }

            return default;
        }

        private async Task UploadNewModels(IEnumerable<TCOBundleModel> latestBundles)
        {
            if (latestBundles != default && latestBundles.Any() == true)
            {
                try
                {
                    await _destinationBundlesRepository.UploadBundles(latestBundles);
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, $"Az új rakatok feltöltése közben hiba lépett fel: {ex.Message}");
                }
            }
        }

        private async Task<DateTime> FetchLatestDate()
        {
            var latestFinishedDate = await _destinationBundlesRepository.GetLatestBundleFinishedDate();
            return GetLatestValidFinshidDate(latestFinishedDate);
        }

        private DateTime GetLatestValidFinshidDate(DateTime actual)
        {
            var latestValidDate = DateTime.Now.AddDays(MaxNumberOfDifferenceDays);
            return actual < latestValidDate ? latestValidDate : actual;
        }
    }
}
