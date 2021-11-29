using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OLM.Services.Auth.Extensions;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Extensions;
using OLM.Services.Bundles.Prices.API.Filters;
using OLM.Services.Bundles.Prices.API.ViewModels.Settings;
using OLM.Shared.Extensions.APIErrorExceptionHandler;
using OLM.Shared.Models.Bundle.Prices.APIResponses;

namespace OLM.Services.Bundles.Prices.API
{
    public class Startup
    {
        public const string DefaultAPIServicesHTTPClientName = "Services";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BundlePriceDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCustomJWTAuth(Configuration)
                    .AddCustomServices()
                    .AddSwaggerServices()
                    .AddAPIServicesHttpClient();

            services.AddSingleton(Configuration.GetSection("ServiceUrls").Get<ServiceURLs>());

            services.AddHealthChecks().AddDbContextCheck<BundlePriceDbContext>();

            services.AddControllers(options =>
            {
                options.Filters.Add<APIErrorExceptionHandlerFilter>();
                options.Filters.Add<ValidationFilter>();
            })
            .AddJsonOptions(options =>
            {

            })
            // ASP.Net Core 2.0 behozta, hogyha a ModelState invalid akkor automatikusan visszatér egy értékkel
            // Ezt a SuppressModelStateInvalidFilter = true; sorral ki lehet javítani
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwaggerMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc");

                endpoints.MapControllers();
            });
        }
    }
}
