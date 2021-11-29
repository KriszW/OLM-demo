using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.BackgroundServices
{
    public class UpdaterBackgroundService : BackgroundService
    {
        private const int WaitingTimeInMSBetweenUpdates = 30000;

        private readonly IUpdaterService _updaterService;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<UpdaterBackgroundService> _logger;

        public UpdaterBackgroundService(IUpdaterService updaterService, IHostApplicationLifetime applicationLifetime, ILogger<UpdaterBackgroundService> logger)
        {
            _updaterService = updaterService;
            _applicationLifetime = applicationLifetime;
            _logger = logger;
        }

        public async override Task StartAsync(CancellationToken cancellationToken)
        {
            await _updaterService.UpdateFinishedDate();

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                await _updaterService.Update();

                await _updaterService.UpdateFinishedDate();

                await Task.Delay(WaitingTimeInMSBetweenUpdates);
            }
        }
    }
}
