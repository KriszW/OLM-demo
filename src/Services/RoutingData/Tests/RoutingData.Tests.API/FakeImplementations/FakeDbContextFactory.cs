using Microsoft.EntityFrameworkCore;
using OLM.Services.RoutingData.API.Data;
using OLM.Services.RoutingData.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.Tests.API.FakeImplementations
{
    public static class FakeDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RoutingDataDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-routingdata-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new RoutingDataDbContext(options);

            await dbContext.AddRangeAsync(Models);
            await dbContext.SaveChangesAsync();
        }

        public static List<BundleDataModel> Models => new List<BundleDataModel>
        {
            new BundleDataModel
            {
                ID = 1,
                Dimension = "19 * 125",
                AllLength = 5123.2,
                FinishedDate = DateTime.Now.AddHours(6).AddMinutes(47),
                MachineName = "1",
            },
            new BundleDataModel
            {
                ID = 2,
                Dimension = "25 * 75",
                AllLength = 6231.2,
                FinishedDate = DateTime.Now.AddHours(7).AddMinutes(23),
                MachineName = "1",
            },
            new BundleDataModel
            {
                ID = 3,
                Dimension = "25 * 75",
                AllLength = 3053.3,
                FinishedDate = DateTime.Now.AddHours(12).AddMinutes(53),
                MachineName = "1",
            },
            new BundleDataModel
            {
                ID = 4,
                Dimension = "19 * 125",
                AllLength = 8502.2,
                FinishedDate = DateTime.Now.AddHours(16).AddMinutes(12),
                MachineName = "1",
            },
            new BundleDataModel
            {
                ID = 5,
                Dimension = "25 * 75",
                AllLength = 1230.5,
                FinishedDate = DateTime.Now.AddHours(21).AddMinutes(6),
                MachineName = "1",
            },

            new BundleDataModel
            {
                ID = 6,
                Dimension = "25 * 75",
                AllLength = 6234.0,
                FinishedDate = DateTime.Now.AddDays(1).AddHours(6).AddMinutes(21),
                MachineName = "1",
            },
            new BundleDataModel
            {
                ID = 7,
                Dimension = "25 * 75",
                AllLength = 2135.2,
                FinishedDate = DateTime.Now.AddDays(1).AddHours(9).AddMinutes(23),
                MachineName = "1",
            },
            new BundleDataModel
            {
                ID = 8,
                Dimension = "25 * 75",
                AllLength = 6423.5,
                FinishedDate = DateTime.Now.AddDays(1).AddHours(13).AddMinutes(43),
                MachineName = "1",
            },
            new BundleDataModel
            {
                ID = 9,
                Dimension = "25 * 75",
                AllLength = 8720.1,
                FinishedDate = DateTime.Now.AddDays(1).AddHours(17).AddMinutes(57),
                MachineName = "1",
            },
            new BundleDataModel
            {
                ID = 10,
                Dimension = "25 * 75",
                AllLength = 2205.6,
                FinishedDate = DateTime.Now.AddDays(1).AddHours(20).AddMinutes(0),
                MachineName = "1",
            },
        };
    }
}
