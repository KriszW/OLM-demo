using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Extensions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Services.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Utilities.RoutingTime;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.BackgroundServices
{
    public class RoutingProdTimeUpdaterBackgroundService : BackgroundService
    {
        private const int WaitingTimeInMSBetweenUpdates = 30000;

        private IProdTimeRepository ProdTimeRepository => _repositoryFactory.CreateProdTime();

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ISettingsTimeFetcherService _settingsTimeFetcherService;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<RoutingProdTimeUpdaterBackgroundService> _logger;

        public RoutingProdTimeUpdaterBackgroundService(IRepositoryFactory repositoryFactory,
                                                       ISettingsTimeFetcherService settingsTimeFetcherService,
                                                       IHostApplicationLifetime hostApplicationLifetime,
                                                       ILogger<RoutingProdTimeUpdaterBackgroundService> logger)
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
                    var hasModelForToday = await ProdTimeRepository.AnyDataBetween(date, end);

                    if (hasModelForToday == false)
                    {
                        var models = _settingsTimeFetcherService.FetchProdTimeModelsForDay(date);

                        if (models != default)
                        {
                            await ProdTimeRepository.Upload(models);
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

                var hasModelForToday = await ProdTimeRepository.AnyDataFor(date);

                if (hasModelForToday == false)
                {
                    var models = _settingsTimeFetcherService.FetchProdTimeModelsForDay(date);

                    await ProdTimeRepository.Upload(models);
                }

                await Task.Delay(WaitingTimeInMSBetweenUpdates);
            }

            _hostApplicationLifetime.StopApplication();
        }
    }
}
