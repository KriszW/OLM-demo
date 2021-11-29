using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OLM.Shared.Utilities.ExcelFileManagerUtility;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.DependencyInjection
{
    public static class CsvDataManagerServiceInjectionExtensions
    {
        public static IServiceCollection AddDbContextCsvDataManager<TDbContext, TModel>(this IServiceCollection services)
            where TDbContext : DbContext
            where TModel : class, new()
        => services.AddScoped<ICSVDataManager<TModel>, DbContextCsvDataManager<TDbContext, TModel>>();

        public static IServiceCollection AddCsvUtility(this IServiceCollection services)
            => services.AddSingleton<ICsvReader, CsvReader>()
                       .AddSingleton<ICsvWriter, CsvWriter>()
                       .AddSingleton<ICsvManager, CsvManager>();
    }
}
