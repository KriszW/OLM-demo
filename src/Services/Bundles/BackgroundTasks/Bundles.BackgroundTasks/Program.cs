using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OLM.Services.Bundles.BackgroundTasks.Updater.Extensions;

namespace OLM.Services.Bundles.BackgroundTasks.Updater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    services.AddCustomServices(configuration)
                            .AddConnectionStringOptions(configuration)
                            .AddDbTableNames(configuration)
                            .AddServiceUrls(configuration);
                });
    }
}
