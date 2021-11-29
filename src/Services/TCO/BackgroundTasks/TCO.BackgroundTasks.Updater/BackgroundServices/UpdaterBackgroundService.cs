using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLM.Services.TCO.BackgroundTasks.Updater.BackgroundServices
{
    public class UpdaterBackgroundService : BackgroundService
    {
        private const int WaitingTimeInMSBetweenUpdates = 30000;

        private readonly IUpdaterService _updaterService;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<UpdaterBackgroundService> _logger;

        public UpdaterBackgroundService(IUpdaterService updaterService,
                                        IHostApplicationLifetime applicationLifetime,
                                        ILogger<UpdaterBackgroundService> logger)
        {
            _updaterService = updaterService;
            _applicationLifetime = applicationLifetime;
            _logger = logger;
        }

        public async override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"A TCO adatok frissítése elindult, a rakatok {WaitingTimeInMSBetweenUpdates / 1000.0} másodpercenként frissülnek");

            try
            {
                await _updaterService.UpdateFinishedDate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"A program indítás közben váratlan hiba lépett fel");

                _logger.LogInformation($"A program leáll....");

                _applicationLifetime.StopApplication();
            }

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                LogStartingIterationOfUpdating();

                try
                {
                    _logger.LogInformation($"A legutolsó frissített TCO adatok időpontjának frissítés {DateTime.Now}-kor");

                    await _updaterService.Update();

                    _logger.LogInformation($"A {_updaterService.LatestFinishedDate}-től a TCO adatok frissítése");

                    await _updaterService.UpdateFinishedDate();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"A TCO adatok frissítése közben nem várt hiba lépett fel, ez a frissítési ciklus kimarad");
                }

                LogWaiting();
                await Task.Delay(WaitingTimeInMSBetweenUpdates);
            }

            LogProgramSuccessfullyClosing();
            _applicationLifetime.StopApplication();
        }

        private void LogStartingIterationOfUpdating()
        {
            _logger.LogDebug($"A frissítés indítása {DateTime.Now}-kor");
            _logger.LogDebug($"A legutolsó frisstett rakat időpontja: {_updaterService.LatestFinishedDate} ");
        }

        private void LogWaiting()
        {
            _logger.LogInformation($"{WaitingTimeInMSBetweenUpdates / 1000.0} másodperc múlva újra frissítés");
            _logger.LogDebug($"Varákozás {WaitingTimeInMSBetweenUpdates / 1000.0} másodpercig...");
        }

        private void LogProgramSuccessfullyClosing()
        {
            _logger.LogInformation($"A TCO adatok frissítésének befejezése");
            _logger.LogInformation($"A program hiba nélkül lefutott");
        }
    }
}
