using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.API.Services.Services.Abstractions;
using OLM.Services.RoutingTime.API.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
                => services.AddScoped<IBundlesRepository, BundlesRepository>()
                           .AddScoped<IProductionTimeRepository, ProductionTimeRepository>()
                           .AddScoped<IPausesRepository, PausesRepository>()
                           .AddScoped<IRoutingTimeCalculaterService, RoutingTimeCalculaterService>();

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "OLM Routing Time microservice API");
                });

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Routing Time microservice API", Version = "v1" });
                });
    }
}
