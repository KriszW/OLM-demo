using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Models;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Services.Implementations
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

        private async Task<IEnumerable<DailyReportModel>> FetchModelsFromLatestFinishedDate()
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

        private async Task UploadNewModels(IEnumerable<DailyReportModel> newSourceModels)
        {
            if (newSourceModels != default && newSourceModels.Any() == true)
            {
                try
                {
                    await _destinationRepository.UploadNewModels(newSourceModels);
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
                var latestDate = await _destinationRepository.GetLatestDate();

                LatestFinishedDate = GetLatestValidFinshidDate(latestDate);

                _logger.LogInformation($"A legfrisebb rakat feltöltési dátuma beállítva: {LatestFinishedDate}");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"A legfrisebb rakat feltöltési dátumának lekérése közben hiba lépett fel: {ex.Message}");
            }
        }

        private DateTime GetLatestValidFinshidDate(DateTime actual)
        {
            var latestValidDate = DateTime.Now.AddDays(MaxNumberOfDifferenceDays);
            return actual < latestValidDate ? latestValidDate : actual;
        }
    }
}
