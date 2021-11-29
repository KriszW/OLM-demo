using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OLM.Services.Auth.Extensions;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Extensions;
using OLM.Services.Bundles.API.Filters;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Services.Abstractions;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Shared.Extensions.APIErrorExceptionHandler;

namespace OLM.Services.Bundles.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BundlesDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCustomJWTAuth(Configuration)
                    .AddCustomServices(Configuration)
                    .AddSwaggerServices();

            services.AddHealthChecks().AddDbContextCheck<BundlesDbContext>();

            services.AddControllers(options => {
                options.Filters.Add<APIErrorExceptionHandlerFilter>();
                options.Filters.Add<ValidationFilter>();
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
