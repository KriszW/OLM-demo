using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLM.Services.Bundles.BackgroundTasks.Updater.BackgroundServices;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Factories.Implementations;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Implementations;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Services.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Services.Implementations;
using OLM.Services.Bundles.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
            => services.AddHttpClient()
                       .AddTransient<ISourceBundlesRepository>(sp =>
                       {
                           var conStrings = sp.GetRequiredService<DbConnections>();
                           var dbTables = configuration.GetSection("TableNames").Get<DbTables>();

                           return new SourceBundlesRepository(new SqlConnection(conStrings.SourceDb), dbTables);
                       })
                       .AddTransient<IDestinationBundlesRepository>(sp =>
                       {
                           var conStrings = sp.GetRequiredService<DbConnections>();
                           var dbTables = configuration.GetSection("TableNames").Get<DbTables>();

                           return new DestinationBundlesRepository(new SqlConnection(conStrings.DestinationDb), dbTables);
                       })
                       .AddTransient<ITCOSourceBundlesRepository, TCOSourceBundlesRepository>(sp =>
                       {

                           var factory = sp.GetRequiredService<IHttpClientFactory>();
                           var serviceUrls = sp.GetRequiredService<ServiceUrls>();

                           return new TCOSourceBundlesRepository(factory, serviceUrls);
                       })
                       .AddTransient<ITCODestinationBundlesRepository, TCODestinationBundlesRepository>(sp =>
                       {

                           var conStrings = sp.GetRequiredService<DbConnections>();
                           var dbTables = configuration.GetSection("TableNames").Get<DbTables>();

                           return new TCODestinationBundlesRepository(new SqlConnection(conStrings.SourceDb), dbTables);
                       })
                       .AddTransient<IUpdaterService, UpdaterService>()
                       .AddTransient<ITCOUpdaterService, TCOUpdaterService>()
                       .AddTransient<IRepositoryFactory, RepositoryFactory>()
                       .AddHostedService<BundlesUpdaterBackgroundService>()
                       .AddHostedService<BundleTCOUpdaterBackgroundService>();

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

        public static IServiceCollection AddServiceUrls(this IServiceCollection services, IConfiguration configuration)
        {
            var model = configuration.GetSection("ServiceUrls").Get<ServiceUrls>();

            return services.AddSingleton(model);
        }
    }
}
