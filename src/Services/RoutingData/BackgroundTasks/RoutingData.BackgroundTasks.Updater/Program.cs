using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OLM.Services.RoutingData.BackgroundTasks.Updater.Extensions;

namespace OLM.Services.RoutingData.BackgroundTasks.Updater
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

                    services.AddCustomServices()
                            .AddConnectionStringOptions(configuration)
                            .AddDbTableNames(configuration);
                });
    }
}
