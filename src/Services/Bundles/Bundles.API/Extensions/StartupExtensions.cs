using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Machine;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Machine;
using OLM.Services.Bundles.API.Services.Services.Abstractions;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Bundles microservice API", Version = "v1" });
            });

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "OLM Bundles microservice API");
            });

        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration config)
             => services.AddScoped<IBundleRepository, BundleRepository>()
                        .AddScoped<IBundlesSumRepository, BundlesSumRepository>()
                        .AddScoped<IMachineBundlesRepository, MachineBundlesRepository>()
                        .AddScoped<IMachineSumRepository, MachineSumRepository>()
                        .AddSingleton<IStartDateProvider, StartDateFromTodayProvider>()
                        .AddSingleton<IEndDateProvider, EndDateFromTodayProvider>()
                        .AddScoped<ITCOBundleRepository>(sp =>
                        {
                            return new TCOBundleRepository(new SqlConnection(config.GetConnectionString("T2XCutConnection")));
                        })
                        .AddScoped<ITCOBundleFileWriterService, TCOBundleFileWriterService>();
    }
}
