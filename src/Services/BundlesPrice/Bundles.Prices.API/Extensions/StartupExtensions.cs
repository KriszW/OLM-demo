using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility;
using OLM.Shared.Utilities.ExcelFileManagerUtility.DependencyInjection;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using OLM.Services.Bundles.Prices.API.Services.Services;
using DocShow.Services.PDFConverter.API.Services.Services.Implementations;
using OLM.Services.Bundles.Prices.API.Services.Services.Abstractions;

namespace OLM.Services.Bundles.Prices.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Bundle Prices microservice API", Version = "v1" });
            });

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "OLM Bundle Prices microservice API");
            });

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
             => services.AddScoped<IBundlePriceManagerRepository, BundlePriceManagerRepository>()
                        .AddScoped<IPricesRepository, PricesRepository>()
                        .AddScoped<IRawBundleIDPriceRepository, RawBundleIDPriceRepository>()
                        .AddScoped<IExchangeRateRepository, ExchangeRateRepository>()
                        .AddSingleton<ICsvManager, BundlePriceConverterCsvManager>()
                        .AddScoped<IExchangeRateRepository, ExchangeRateRepository>()
                        .AddScoped<IBundlePriceCsvDataManager, BundlePricesCsvDataManager>();
    }
}
