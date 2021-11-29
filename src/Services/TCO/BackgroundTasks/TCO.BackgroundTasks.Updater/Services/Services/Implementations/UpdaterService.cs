using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OLM.Services.TCO.BackgroundTasks.Updater.Extensions;
using OLM.Services.TCO.BackgroundTasks.Updater.Models;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.TCO.BackgroundTasks.Updater.Services.Services.Implementations
{
    public class UpdaterService : IUpdaterService
    {
        private const int MaxNumberOfDifferenceDays = -8;

        private IDestinationRepository _destinationRepository => _repositoryFactory.CreateDestination();
        private ISourceRepository _sourceRepository => _repositoryFactory.CreateSource();

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILogger<UpdaterService> _logger;

        public UpdaterService(IRepositoryFactory repositoryFactory, ILogger<UpdaterService> logger)
        {
            _repositoryFactory = repositoryFactory;
            _logger = logger;
        }

        public DateTime LatestFinishedDate { get; set; }

        public async Task Update()
        {
            var latestBundles = await FetchModelsFromLatestFinishedDate();

            await UploadNewModels(latestBundles);
        }

        private async Task<IEnumerable<TCODataSourceModel>> FetchModelsFromLatestFinishedDate()
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

        private async Task UploadNewModels(IEnumerable<TCODataSourceModel> newSourceModels)
        {
            if (newSourceModels != default && newSourceModels.Any() == true)
            {
                try
                {
                    await _destinationRepository.UploadNewModels(newSourceModels.Select(m => m.ConvertToDestinationModel()));
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
            var latestFinishedDate = await _sourceRepository.GetDatetimeForBundle(bundleID);
            return GetLatestValidFinshidDate(latestFinishedDate);
        }

        private DateTime GetLatestValidFinshidDate(DateTime actual)
        {
            var latestValidDate = DateTime.Now.AddDays(MaxNumberOfDifferenceDays);
            return actual < latestValidDate ? latestValidDate : actual;
        }
    }
}
