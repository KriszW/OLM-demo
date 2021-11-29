using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Models;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Services.Implementations
{
    public class UpdaterService : IUpdaterService
    {
        private const int MaxNumberOfDifferenceDays = -8;

        private IDestinationRepository _destinationRepository => _repoFactory.CreateDestination();
        private ISourceRepository _sourceRepository => _repoFactory.CreateSource();

        private readonly IRepositoryFactory _repoFactory;
        private readonly ILogger<UpdaterService> _logger;

        public UpdaterService(IRepositoryFactory repoFactory, ILogger<UpdaterService> logger)
        {
            _repoFactory = repoFactory;
            _logger = logger;
        }

        public DateTime LatestFinishedDate { get; set; }

        public async Task Update()
        {
            var latestBundles = await FetchModelsFromLatestFinishedDate();

            await UploadNewModels(latestBundles);
        }

        private async Task<IEnumerable<SourceModel>> FetchModelsFromLatestFinishedDate()
        {
            try
            {
                return await _sourceRepository.GetModelsFrom(LatestFinishedDate);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"A rakatok lekérdezés {LatestFinishedDate}-től query közben hiba lépett fel: {ex.Message}");
            }

            return default;
        }

        private async Task UploadNewModels(IEnumerable<SourceModel> newSourceModels)
        {
            if (newSourceModels != default && newSourceModels.Any() == true)
            {
                try
                {
                    var bundles = newSourceModels.GroupBy(m => m.BundleID).Select(m => new DestinationBundleModel()
                    {
                        ID = default,
                        BundleID = m.Key,
                        Itemnumbers = m.Select(sm => new DestinationItemnumberModel()
                        {
                            ID = default,
                            BundleItemnumbersModelID = default,
                            Itemnumber = sm.Itemnumber
                        })
                    });

                    await _destinationRepository.UploadNewModels(bundles);
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, $"Az új rakatok feltöltése közben hiba lépett fel: {ex.Message}");
                }
            }
        }

        public async Task UpdateFinishedDate()
        {
            try
            {
                var latestBundleID = await _destinationRepository.GetLatestBundleID();

                if (latestBundleID != (default))
                {
                    LatestFinishedDate = await FetchLatestDate(latestBundleID);
                }
                else
                {
                    LatestFinishedDate = GetLatestValidFinshidDate(new DateTime());
                }

                _logger.LogInformation($"A legfrisebb rakat feltöltési dátuma beállítva: {LatestFinishedDate}");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"A legfrisebb rakat feltöltési dátumának lekérése közben hiba lépett fel: {ex.Message}");
            }
        }

        private async Task<DateTime> FetchLatestDate(string bundleID)
        {
            var latestFinishedDate = await _sourceRepository.GetDateForBundleID(bundleID);
            return GetLatestValidFinshidDate(latestFinishedDate);
        }

        private DateTime GetLatestValidFinshidDate(DateTime actual)
        {
            var latestValidDate = DateTime.Now.AddDays(MaxNumberOfDifferenceDays);
            return actual < latestValidDate ? latestValidDate : actual;
        }
    }
}
