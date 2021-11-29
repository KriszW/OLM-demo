using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OLM.Services.RoutingData.BackgroundTasks.Updater.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.BackgroundTasks.Updater.BackgroundServices
{
    public class RoutingDataUpdaterBackgroundService : BackgroundService
    {
        private const int WaitingTimeInMSBetweenUpdates = 30000;

        private ILogger<RoutingDataUpdaterBackgroundService> _logger;
        private IHostApplicationLifetime _applicationLifetime;
        private IUpdaterService _uploaderService;

        public RoutingDataUpdaterBackgroundService(ILogger<RoutingDataUpdaterBackgroundService> logger,
                                                   IHostApplicationLifetime applicationLifetime,
                                                   IUpdaterService uploaderService)
        {
            _logger = logger;
            _applicationLifetime = applicationLifetime;
            _uploaderService = uploaderService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"A rakatok frissítése elindult, a rakatok {WaitingTimeInMSBetweenUpdates / 1000.0} másodpercenként frissülnek");
            try
            {
                await _uploaderService.UpdateFinishedDate();
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
                    _logger.LogInformation($"A legutolsó frissített rakat időpontjának frissítés {DateTime.Now}-kor");

                    await _uploaderService.Update();

                    _logger.LogInformation($"A {_uploaderService.LatestFinishedDate}-től a rakatok frissítése");

                    await _uploaderService.UpdateFinishedDate();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"A rakatok frissítése közben nem várt hiba lépett fel, ez a frissítési ciklus kimarad");
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
            _logger.LogDebug($"A legutolsó frisstett rakat időpontja: {_uploaderService.LatestFinishedDate} ");
        }

        private void LogWaiting()
        {
            _logger.LogInformation($"{WaitingTimeInMSBetweenUpdates / 1000.0} másodperc múlva újra frissítés");
            _logger.LogDebug($"Varákozás {WaitingTimeInMSBetweenUpdates / 1000.0} másodpercig...");
        }

        private void LogProgramSuccessfullyClosing()
        {
            _logger.LogInformation($"A rakat frissítésének befejezése");
            _logger.LogInformation($"A program hiba nélkül lefutott");
        }
    }
}
