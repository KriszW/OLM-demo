using Microsoft.EntityFrameworkCore;
using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OLM.Services.Routing.Tests.API.FakeImplementations
{
    public static class FakeDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RoutingDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-routing-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new RoutingDbContext(options);

            await dbContext.AddRangeAsync(Models);
            await dbContext.SaveChangesAsync();
        }

        public static List<RoutingModel> Models = new List<RoutingModel>
        {
            new RoutingModel
            {
                ID = 1,
                Dimension = "19 * 75",
                CycleQuantityPerMinute = 37.293,
            },
            new RoutingModel
            {
                ID = 2,
                Dimension = "19 * 100",
                CycleQuantityPerMinute = 28.872,
            },
            new RoutingModel
            {
                ID = 3,
                Dimension = "25 * 75",
                CycleQuantityPerMinute = 36.491,
            },
            new RoutingModel
            {
                ID = 4,
                Dimension = "25 * 100",
                CycleQuantityPerMinute = 41.704,
            },
        };
    }
}
