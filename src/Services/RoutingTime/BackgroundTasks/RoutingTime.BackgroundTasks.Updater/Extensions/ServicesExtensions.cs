using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.BackgroundServices;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Factories.Implementations;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Services.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Services.Implementations;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Utilities.RoutingTime;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
            => services.AddTransient<ISourceRepository>(sp =>
            {
                var conStrings = sp.GetRequiredService<DbConnections>();
                var dbTables = sp.GetRequiredService<DbTables>();

                return new SourceRepository(new SqlConnection(conStrings.SourceDb), dbTables);
            })
            .AddTransient<IDestinationRepository>(sp =>
            {
                var conStrings = sp.GetRequiredService<DbConnections>();
                var dbTables = sp.GetRequiredService<DbTables>();

                return new DestinationRepository(new SqlConnection(conStrings.DestinationDb), dbTables);
            })
            .AddTransient<IPausesRepository>(sp => 
            {
                var conStrings = sp.GetRequiredService<DbConnections>();

                return new PausesRepository(new SqlConnection(conStrings.DestinationDb));
            })
            .AddTransient<IProdTimeRepository>(sp =>
            {
                var conStrings = sp.GetRequiredService<DbConnections>();

                return new ProdTimeRepository(new SqlConnection(conStrings.DestinationDb));
            })
            .AddTransient<ISettingsTimeFetcherService, SettingsTimeFetcherService>()
            .AddTransient<IUpdaterService, UpdaterService>()
            .AddTransient<IRepositoryFactory, RepositoryFactory>()
            .AddHostedService<RoutingTimeUpdaterBackgroundService>()
            .AddHostedService<RoutingProdTimeUpdaterBackgroundService>()
            .AddHostedService<RoutingPauseTimeUpdaterBackgroundService>();

        public static IServiceCollection AddConnectionStringOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var model = configuration.GetSection("DbConnections").Get<DbConnections>();

            return services.AddSingleton(model);
        }

        public static IServiceCollection AddDbTableNames(this IServiceCollection services, IConfiguration configuration)
        {
            var model = configuration.GetSection("TableNames").Get<DbTables>();

            return services.AddSingleton(model);
        }

        public static IServiceCollection AddRoutingTimeOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var model = configuration.GetSection("RoutingTimes").Get<RoutingTimeSettings>();

            return services.AddSingleton(model);
        }
    }
}
