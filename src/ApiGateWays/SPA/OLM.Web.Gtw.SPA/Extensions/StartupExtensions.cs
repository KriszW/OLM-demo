using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Eureka;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.OneMachine;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.SummarizedMachines;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations.OneMachine;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations.SummarizedMachines;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Implementations;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.Routing.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.Routing.Implementations;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.TCOBundle.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.TCOBundle.Implementations;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.BundlePrices;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.CategoryBulbs;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.MoneyExchange;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Routing;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Tram;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.BundlePrices;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.CategoryBulbs;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.MoneyExchange;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.Routing;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.TCO;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.Tram;
using OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings;
using OLM.Shared.Extensions.Caching.Abstractions;
using OLM.Shared.Extensions.Caching.Implementations;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using System.Text;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Extensions
{
    public static class StartupExtensions
    {
        private const string OcelotProviderKey = "Ocelot";

        public static IServiceCollection AddOcelotCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("Secret"));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(OcelotProviderKey, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    AuthenticationType = "Identity.Application"
                };

            });

            return services;
        }

        public static IServiceCollection AddOcelotServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOcelotCustomAuth(configuration)
                    .AddOcelot(configuration)
                    .AddEureka();

            return services;
        }

        public static IServiceCollection AddServiceAccessUrls(this IServiceCollection services, IConfiguration configuration)
        {
            var model = configuration.GetSection("ServiceUrls").Get<ServiceUrlSettings>();

            return services.AddScoped((services) => model);
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) 
         => services.AddScoped<IBundleMachineAggregator, BundleMachineAggregator>()
                    .AddScoped<ILatestBundleWithBundlePriceForMachineAggregator, LatestBundleWithBundlePriceForMachineAggregator>()
                    .AddScoped<IDailyBundlesWithBundlePriceForMachineAggregator, DailyBundlesWithBundlePriceForMachineAggregator>()
                    .AddScoped<IWeeklyBundlesWithBundleIDForMachineAggregator, WeeklyBundlesWithBundlePriceForMachineAggregator>()
                    .AddScoped<ISummarizedDailyBundlesWithBundlePriceForMachinesAggreagator, SummarizedDailyBundlesWithBundlePriceForMachinesAggreagator>()
                    .AddScoped<ISummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator, SummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator>()
                    .AddScoped<IFetchOneMachinesBundleService, FetchOneMachinesBundleWithHttpClientService>()
                    .AddScoped<IDailyReportAggregator, HttpDailyReportAggregator>()
                    .AddScoped<IDailyReportService, HttpDailyReportService>()
                    .AddScoped<IWeeklyReportService, HttpWeeklyReportService>()
                    .AddScoped<IWeeksReportService, HttpWeeksReportService>()
                    .AddScoped<IDailyReportFileDownloaderAggregator, HttpDailyReportFileDownloaderAggregator>()
                    .AddScoped<IDailyReportFileDownloadService, HttpDailyReportFileDownloadService>()
                    .AddScoped<IRoutingAggregator, RoutingAggregator>()
                    .AddScoped<IDailyRoutingService, HttpDailyRoutingService>()
                    .AddScoped<IWeeklyRoutingService, HttpWeeklyRoutingService>()
                    .AddScoped<IRoutingService, HttpRoutingService>()
                    .AddScoped<IValidateOneBundleService, HttpBundleValidatorService>()
                    .AddScoped<ICurrencyExchangeService, HttpCurrencyExchangeService>()
                    .AddScoped<IBundlePriceFileUploadAggregator, HttpBundlePriceFileUploadAggregator>()
                    .AddScoped<IBundlePriceFileUploadService, HttpBundlePriceFileUploadService>()
                    .AddScoped<IFetchSummarizedBundleService, FetchSummarizedBundleWithHttpClientService>()
                    .AddScoped<ITramService, HttpFetchTramService>()
                    .AddScoped<IFetchBundlePriceService, HttpFetchBundlePriceService>()
                    .AddScoped<ITCOCalculatorService, FetchCalculatedTCOService>()
                    .AddScoped<IFetchRawTCOCalculatorService, FetchRawTCOCalculatorService>()
                    .AddScoped<ITCOBundleAggregator, TCOBundleAggregator>()
                    .AddScoped<IFetchBundleService, FetchBundleService>()
                    .AddScoped<IFetchTCODataService, FetchTCODataService>()
                    .AddSingleton<ICachingService<string, MachineViewModel>, CachingService<string, MachineViewModel>>()
                    .AddSingleton<ICachingService<string, SummarizedMachineViewModel>, CachingService<string, SummarizedMachineViewModel>>();
    }
}
