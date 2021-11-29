using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Services.Implementations;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Abstractions;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Services.Services.Implementations;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.DependencyInjection;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
                => services.AddScoped<ITramDataRepository, TramDataRepository>()
                           .AddScoped<ITramDimensionRepository, TramDimensionRepository>()
                           .AddScoped<ITramsRepository, TramsRepository>()
                           .AddSingleton<ICsvManager, TramCsvManager>()
                           .AddScoped<ICSVDataManager<TramDataModel>, TramCsvDataManager>();

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "OLM Trams microservice API");
                });

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Trams microservice API", Version = "v1" });
                });
    }
}
