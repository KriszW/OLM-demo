using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Tests.FakeImplementations
{
    public static class FakeBundlesDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<BundlesDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-bundles-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new BundlesDbContext(options);

            await dbContext.AddRangeAsync(CreateModels());
            await dbContext.SaveChangesAsync();
        }


        public static async Task DeleteAll(DbContextOptions options)
        {
            using var dbContext = new BundlesDbContext(options);

            dbContext.RemoveRange(await dbContext.Bundles.Where(b => b.ID > -1).ToListAsync());
            await dbContext.SaveChangesAsync();
        }

        public static List<BundleModel> CreateModels() => new List<BundleModel>()
        {
            new BundleModel()
            {
                BundleID = "test1",
                Input = 3463,
                FS = 233,
                Produced = 3000,
                Waste = 230,
                MachineName = "0",
                FinishedDate = DateTime.Now.AddDays(-1)
            },
            new BundleModel()
            {
                BundleID = "test4",
                Input = 3463,
                FS = 233,
                Produced = 3000,
                Waste = 230,
                MachineName = "0",
                FinishedDate = DateTime.Now.AddDays(-10)
            },
            new BundleModel()
            {
                BundleID = "test2",
                Input = 3245,
                FS = 140,
                Produced = 2800,
                Waste = 305,
                MachineName = "0",
                FinishedDate = DateTime.Now
            },
            new BundleModel()
            {
                BundleID = "test3",
                Input = 3500,
                FS = 400,
                Produced = 2900,
                Waste = 200,
                MachineName = "2",
                FinishedDate = DateTime.Now
            },
            new BundleModel()
            {
                BundleID = "test5",
                Input = 3463,
                FS = 233,
                Produced = 3000,
                Waste = 230,
                MachineName = "2",
                FinishedDate = DateTime.Now.AddDays(-10)
            },
        };
    }
}
