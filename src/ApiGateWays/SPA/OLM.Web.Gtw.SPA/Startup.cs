using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Primitives;
using OLM.Services.Auth.Extensions;
using FluentValidation.AspNetCore;
using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Filters;
using OLM.Shared.Extensions.APIErrorExceptionHandler;

namespace OLM.ApiGateWays.Web.Gtw.SPA
{
    public class Startup
    {
        public const string DefaultAPIServicesHTTPClientName = "Services";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private const string MyAllowSpecificOrigins = "blazor";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => {
                options.Filters.Add<APIErrorExceptionHandlerFilter>();
                options.Filters.Add<ValidationFilter>();
            })
            // ASP.Net Core 2.0 behozta, hogyha a ModelState invalid akkor automatikusan visszat?r egy ?rt?kkel
            // Ezt a SuppressModelStateInvalidFilter = true; sorral ki lehet jav?tani
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyOrigin();
                                      builder.AllowAnyMethod();
                                      builder.SetIsOriginAllowedToAllowWildcardSubdomains();
                                  });
            });

            services.AddCustomJWTAuth(Configuration)
                    .AddCustomServices()
                    .AddServiceAccessUrls(Configuration)
                    .AddAPIServicesHttpClient()
                    .AddOcelotServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
        }
    }
}
