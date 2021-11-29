using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Models;
using OLM.Services.Target.API.Services.Repositories.Abstractions;
using OLM.Services.Target.API.Services.Repositories.Implementations;
using OLM.Shared.Utilities.ExcelFileManagerUtility.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
            => services.AddScoped<IWasteTargetRepository, WasteTargetRepository>()
                       .AddScoped<ITargetRepository, TargetRepository>()
                       .AddCsvUtility()
                       .AddDbContextCsvDataManager<TargetDbContext, WasteTargetDataModel>();

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "OLM Target microservice API");
            });

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Target microservice API", Version = "v1" });
            });
    }
}
