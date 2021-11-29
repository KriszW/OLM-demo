using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Extensions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.BackgroundServices
{
    public class RoutingPauseTimeUpdaterBackgroundService : BackgroundService
    {
        private const int WaitingTimeInMSBetweenUpdates = 30000;

        private IPausesRepository _pausesRepository => _repositoryFactory.CreatePauses();

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ISettingsTimeFetcherService _settingsTimeFetcherService;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<RoutingPauseTimeUpdaterBackgroundService> _logger;

        public RoutingPauseTimeUpdaterBackgroundService(IRepositoryFactory repositoryFactory,
                                                        ISettingsTimeFetcherService settingsTimeFetcherService,
                                                        IHostApplicationLifetime hostApplicationLifetime,
                                                        ILogger<RoutingPauseTimeUpdaterBackgroundService> logger)
        {
            _repositoryFactory = repositoryFactory;
            _settingsTimeFetcherService = settingsTimeFetcherService;
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var days = DateTime.Now.GetDaysUntilTheStartOfTheWeek(DayOfWeek.Monday);

            if (days.Any() == true)
            {
                var end = days.First().AddDays(1);

                foreach (var date in days)
                {
                    var hasModelForToday = await _pausesRepository.AnyDataBetween(date, end);

                    if (hasModelForToday == false)
                    {
                        var models = _settingsTimeFetcherService.FetchPauseModelsForDay(date);

                        if (models != default)
                        {
                            await _pausesRepository.Upload(models);
                        }
                    }

                    end = date;
                }
            }

            await base.StartAsync(cancellationToken);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                var date = DateTime.Now;

                var hasModelForToday = await _pausesRepository.AnyDataFor(date);

                if (hasModelForToday == false)
                {
                    var models = _settingsTimeFetcherService.FetchPauseModelsForDay(date);

                    await _pausesRepository.Upload(models);
                }

                await Task.Delay(WaitingTimeInMSBetweenUpdates);
            }

            _hostApplicationLifetime.StopApplication();
        }
    }
}
