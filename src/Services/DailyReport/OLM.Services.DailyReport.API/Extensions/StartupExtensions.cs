using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Daily;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weekly;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weeks;
using OLM.Services.DailyReport.API.Services.Services.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Daily;
using OLM.Services.DailyReport.API.Services.Services.Implementations.FileManager;
using OLM.Services.DailyReport.API.Services.Services.Implementations.FileManager.Common;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Weekly;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Weeks;
using OLM.Services.DailyReport.API.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
            => services.AddScoped<IDailyReportRepository, DailyReportRepository>()
                       .AddScoped<ITargetRepository, HttpFetchTargetRepository>()
                       .AddScoped<IDailyReportWorkBookWriter, DailyReportWorkBookWriter>()
                       .AddScoped<ITargetWorkBookWriter, TargetWorkBookWriter>()
                       .AddScoped<ITramModelWorkBookWriter, TramModelWorkBookWriter>()
                       .AddScoped<IDailyReportFileWriter, DailyReportFileWriter>()
                       .AddScoped<IWeeklyReportFileWriter, WeeklyReportFileWriter>()
                       .AddScoped<IWeeksReportFileWriter, WeeksReportFileWriter>()
                       .AddScoped<IWeeklyReportRepository, WeeklyReportRepository>()
                       .AddScoped<IWeeksReportRepository, WeeksReportRepository>()
                       .AddScoped<IWeeklyReportFetchService, WeeklyReportFetchService>()
                       .AddScoped<IWeeklyReportDataAggregatorService, WeeklyReportDataAggregatorService>()
                       .AddScoped<IWeeksReportFetchService, WeeksReportFetchService>()
                       .AddScoped<IWeeksReportDataAggregatorService, WeeksReportDataAggregatorService>()
                       .AddScoped<IDailyReportDataFetchService, DailyReportDataFetchService>()
                       .AddScoped<IDailyReportDataAggregationProviderService, DailyReportDataAggregationProviderService>()
                       .AddScoped<IDailyReportFileWriter, DailyReportFileWriter>();

        public static IServiceCollection AddServiceAccessUrls(this IServiceCollection services, IConfiguration configuration)
        {
            var model = configuration.GetSection("ServiceUrls").Get<ServiceUrls>();

            return services.AddSingleton((services) => model);
        }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "OLM Daily Report microservice API");
            });

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Daily Report microservice API", Version = "v1" });
            });
    }
}
