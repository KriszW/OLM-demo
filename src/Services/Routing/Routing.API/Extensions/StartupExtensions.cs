using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.Routing.API.Services.Repositories.Abstractions;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.API.Services.Services.Abstractions;
using OLM.Services.Routing.API.Services.Services.Implementations;
using OLM.Services.Routing.API.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
                => services.AddAPIServicesHttpClient()
                           .AddScoped<IRoutingRepository, RoutingRepository>()
                           .AddScoped<IRoutingDataRepository, RoutingDataRepository>()
                           .AddScoped<IRoutingManagerRepository, RoutingManagerRepository>()
                           .AddScoped<IRoutingTimeRepository, RoutingTimeRepository>()
                           .AddScoped<IRoutingDataRepository, RoutingDataRepository>()
                           .AddScoped<IRoutingService, RoutingService>();

        public static IServiceCollection AddUrlsOfService(this IServiceCollection services, IConfiguration configuration)
        {
            var model = configuration.GetSection("ServiceUrls").Get<ServicesSettings>();

            return services.AddScoped((services) => model);
        }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "OLM Routing microservice API");
                });

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Routing microservice API", Version = "v1" });
                });
    }
}
