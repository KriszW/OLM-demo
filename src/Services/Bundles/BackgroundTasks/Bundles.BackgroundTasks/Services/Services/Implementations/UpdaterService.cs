using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Services.Implementations
{
    // TODO : Add better logging
    public class UpdaterService : IUpdaterService
    {
        private const int MaxNumberOfDifferenceDays = -60;
        private ISourceBundlesRepository _sourceBundlesRepository => _repoFactory.CreateSourceRepo();
        private IDestinationBundlesRepository _destinationBundlesRepository => _repoFactory.CreateDestinationRepo();

        public DateTime LatestFinishedDate { get; set; }

        private readonly IRepositoryFactory _repoFactory;
        private readonly ILogger<UpdaterService> _logger;

        public UpdaterService(IRepositoryFactory repoFactory,
                              ILogger<UpdaterService> logger)
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
            var latestBundles = await FetchModelsFromLatestFinishedDate();

            await UploadNewModels(latestBundles);
        }

        private async Task<IEnumerable<BundleModel>> FetchModelsFromLatestFinishedDate()
        {
            try
            {
                return await _sourceBundlesRepository.QueryBundlesFromDate(LatestFinishedDate);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"A rakatok lekérdezés {LatestFinishedDate}-től query közben hiba lépett fel: {ex.Message}");
            }

            return default;
        }

        private async Task UploadNewModels(IEnumerable<BundleModel> latestBundles)
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
