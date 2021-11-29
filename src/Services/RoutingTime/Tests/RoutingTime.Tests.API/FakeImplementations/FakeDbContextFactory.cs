using Microsoft.EntityFrameworkCore;
using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.Tests.API.FakeImplementations
{
    public static class FakeDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RoutingTimeDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-routingtime-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new RoutingTimeDbContext(options);

            await dbContext.AddRangeAsync(CreateData);
            await dbContext.AddRangeAsync(CreatePauses());
            await dbContext.AddRangeAsync(CreateProductionTimes());
            await dbContext.SaveChangesAsync();
        }

        public static List<BundleModel> CreateData = new List<BundleModel>
        {
            new BundleModel
            {
                ID = 1,
                Dimension = "19 * 125",
                FinishedDate = DateTime.Now.Date.AddHours(6).AddMinutes(47),
                MachineName = "1",
            },
            new BundleModel
            {
                ID = 2,
                Dimension = "25 * 75",
                FinishedDate = DateTime.Now.Date.AddHours(7).AddMinutes(23),
                MachineName = "1",
            },
            new BundleModel
            {
                ID = 3,
                Dimension = "25 * 75",
                FinishedDate = DateTime.Now.Date.AddHours(12).AddMinutes(53),
                MachineName = "1",
            },
            new BundleModel
            {
                ID = 4,
                Dimension = "19 * 125",
                FinishedDate = DateTime.Now.Date.AddHours(16).AddMinutes(12),
                MachineName = "1",
            },
            new BundleModel
            {
                ID = 5,
                Dimension = "25 * 75",
                FinishedDate = DateTime.Now.Date.AddHours(21).AddMinutes(6),
                MachineName = "1",
            },

            new BundleModel
            {
                ID = 6,
                Dimension = "25 * 75",
                FinishedDate = DateTime.Now.AddDays(1).Date.AddHours(6).AddMinutes(21),
                MachineName = "1",
            },
            new BundleModel
            {
                ID = 7,
                Dimension = "25 * 75",
                FinishedDate = DateTime.Now.AddDays(1).Date.AddHours(9).AddMinutes(23),
                MachineName = "1",
            },
            new BundleModel
            {
                ID = 8,
                Dimension = "25 * 75",
                FinishedDate = DateTime.Now.AddDays(1).Date.AddHours(13).AddMinutes(43),
                MachineName = "1",
            },
            new BundleModel
            {
                ID = 9,
                Dimension = "25 * 75",
                FinishedDate = DateTime.Now.AddDays(1).Date.AddHours(17).AddMinutes(57),
                MachineName = "1",
            },
            new BundleModel
            {
                ID = 10,
                Dimension = "25 * 75",
                FinishedDate = DateTime.Now.AddDays(1).Date.AddHours(20).AddMinutes(0),
                MachineName = "1",
            },
        };

        public static List<PauseModel> CreatePauses()
        {
            var weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            return new List<PauseModel>
            {
                new PauseModel
                {
                    Day = DateTime.Now.DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddHours(8),
                    End = DateTime.Now.Date.AddHours(8).AddMinutes(5)
                },
                new PauseModel
                {
                    Day = DateTime.Now.DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddHours(10),
                    End = DateTime.Now.Date.AddHours(10).AddMinutes(20)
                },
                new PauseModel
                {
                    Day = DateTime.Now.DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddHours(16),
                    End = DateTime.Now.Date.AddHours(16).AddMinutes(5)
                },
                new PauseModel
                {
                    Day = DateTime.Now.DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddHours(18),
                    End = DateTime.Now.Date.AddHours(18).AddMinutes(20)
                },
                new PauseModel
                {
                    Day = DateTime.Now.DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddHours(20),
                    End = DateTime.Now.Date.AddHours(20).AddMinutes(5)
                },

                new PauseModel
                {
                    Day = DateTime.Now.DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddDays(1).AddHours(8),
                    End = DateTime.Now.Date.AddDays(1).AddHours(8).AddMinutes(5)
                },
                new PauseModel
                {
                    Day = DateTime.Now.AddDays(1).DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddDays(1).AddHours(10),
                    End = DateTime.Now.Date.AddDays(1).AddHours(10).AddMinutes(20)
                },
                new PauseModel
                {
                    Day = DateTime.Now.AddDays(1).DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddDays(1).AddHours(16),
                    End = DateTime.Now.Date.AddDays(1).AddHours(16).AddMinutes(5)
                },
                new PauseModel
                {
                    Day = DateTime.Now.AddDays(1).DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddDays(1).AddHours(18),
                    End = DateTime.Now.Date.AddDays(1).AddHours(18).AddMinutes(20)
                },
                new PauseModel
                {
                    Day = DateTime.Now.AddDays(1).DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddDays(1).AddHours(20),
                    End = DateTime.Now.Date.AddDays(1).AddHours(20).AddMinutes(5)
                },
            };
        }


        public static List<ProductionTimeModel> CreateProductionTimes() 
        {
            var weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            return new List<ProductionTimeModel>
            {
                new ProductionTimeModel
                {
                    Day = DateTime.Now.DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddHours(6),
                    End = DateTime.Now.Date.AddHours(22),
                },
                new ProductionTimeModel
                {
                    Day = DateTime.Now.AddDays(1).DayOfWeek,
                    WeekNumber = weekNumber,
                    MachineName = "1",
                    Start = DateTime.Now.Date.AddDays(1).AddHours(6),
                    End = DateTime.Now.Date.AddDays(1).AddHours(22),
                }
            };
        } 
    }
}
