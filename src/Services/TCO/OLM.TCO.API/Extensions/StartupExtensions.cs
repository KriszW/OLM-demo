using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Abstractions;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.ViewModels.Settings;
using OLM.Shared.Utilities.ExcelFileManagerUtility.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
                => services.AddScoped<ITCOCalculationProviderService, EuroTCOCalculationProvider>()
                           .AddScoped<ITCOCalculatorFromBundlesService, EuroTCOCalculatorService>()
                           .AddScoped<IBundleTCOAggregatorService, BundleTCOAggregatorService>()
                           .AddScoped<ITCOSettingsRepository, TCOSettingsRepository>()
                           .AddScoped<IRawTCOAggregatorService, RawTCOAggregatorService>()
                           .AddScoped<IRawTCOBundleSettingsAggregatorService, RawTCOBundleSettingsAggregatorService>()
                           .AddScoped<IRawTCOCalculatorService, RawTCOCalculatorService>()
                           .AddScoped<ITCODataRepository, TCODataRepository>()
                           .AddScoped<IBundlePriceRepository, BundlePriceAPIRepository>()
                           .AddCsvUtility()
                           .AddDbContextCsvDataManager<TCODataDbContext, TCOValueSettingsModel>();

        public static IServiceCollection AddServiceAccessUrls(this IServiceCollection services, IConfiguration configuration)
        {
            var model = configuration.GetSection("ServiceUrls").Get<ServiceUrls>();

            return services.AddSingleton((services) => model);
        }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "OLM TCO microservice API");
                });

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM TCO microservice API", Version = "v1" });
                });
    }
}
