using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Money Exchange Rate microservice API", Version = "v1" });
            });

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "OLM Money Exchange Rate microservice API");
            });
    }
}
