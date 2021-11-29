using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Models;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Abstractions;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations;
using OLM.Shared.Utilities.ExcelFileManagerUtility.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services) 
            => services.AddScoped<IBundleItemnumberRepository, BundleItemnumberRepository>()
                       .AddScoped<IItemNumberCategoryRepository, ItemnumberCategoryRepository>()
                       .AddCsvUtility()
                       .AddDbContextCsvDataManager<CategoryBulbsDbContext, ItemnumberCategoryModel>();

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OLM Bundle Prices microservice API", Version = "v1" });
            });

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
            => app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "OLM Bundle Prices microservice API");
            });
    }
}
