using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.RoutingData.API.Services.Repositories.Abstractions;
using OLM.Services.RoutingData.API.Services.Repositories.Implementations;
using OLM.Services.RoutingData.API.Services.Services.Abstractions;
using OLM.Services.RoutingData.API.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
                => services.AddScoped<IRoutingDataRepository, RoutingDataRepository>()
                           .AddScoped<IRoutingDataCalculatorService, RoutingDataCalculatorService>();

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "OLM Routing Data microservice API");
                });

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Routing Data microservice API", Version = "v1" });
                });
    }
}
