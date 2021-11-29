using Microsoft.EntityFrameworkCore;
using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Tests.FakeImplementations
{
    public static class FakeDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<TargetDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-target-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new TargetDbContext(options);

            await dbContext.AddRangeAsync(CreateTargets());
            await dbContext.SaveChangesAsync();
        }

        public static List<WasteTargetDataModel> CreateTargets() => new List<WasteTargetDataModel>
        {
            new WasteTargetDataModel
            {
                Dimension = "25x75",
                Target = 0.1671,
            },
            new WasteTargetDataModel
            {
                Dimension = "19x125",
                Target = 0.1,
            },
            new WasteTargetDataModel
            {
                Dimension = "38x75",
                Target = 0.21,
            },
            new WasteTargetDataModel
            {
                Dimension = "13x25",
                Target = 0.21,
            }
        };
    }
}
