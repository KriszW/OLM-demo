using Microsoft.EntityFrameworkCore;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Tests.FakeImplementations
{
    public static class FakeDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<DailyReportDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-tco-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new DailyReportDbContext(options);

            await dbContext.AddRangeAsync(CreateReportData());
            await dbContext.SaveChangesAsync();
        }

        public static List<DailyReportDataModel> CreateReportData() => new List<DailyReportDataModel>
        {
            new DailyReportDataModel
            {
                Dimension = "25x75",
                Date = DateTime.Now,
                Length = 5000,
                LengthOfFS = 500,
                LengthOfWaste = 1500,
            },
            new DailyReportDataModel
            {
                Dimension = "25x75",
                Date = DateTime.Now,
                Length = 4512,
                LengthOfFS = 134,
                LengthOfWaste = 561,
            },
            new DailyReportDataModel
            {
                Dimension = "19x125",
                Date = DateTime.Now,
                Length = 2145,
                LengthOfFS = 0,
                LengthOfWaste = 140,
            },
            new DailyReportDataModel
            {
                Dimension = "38x75",
                Date = DateTime.Now,
                Length = 4156,
                LengthOfFS = 1236,
                LengthOfWaste = 510,
            },
            new DailyReportDataModel
            {
                Dimension = "19x125",
                Date = DateTime.Now,
                Length = 1245,
                LengthOfFS = 76,
                LengthOfWaste = 100,
            },
            new DailyReportDataModel
            {
                Dimension = "13x25",
                Date = DateTime.Now,
                Length = 5106,
                LengthOfFS = 1235,
                LengthOfWaste = 0,
            },
            new DailyReportDataModel
            {
                Dimension = "19x125",
                Date = DateTime.Now,
                Length = 3510,
                LengthOfFS = 500,
                LengthOfWaste = 1240,
            },
            new DailyReportDataModel
            {
                Dimension = "38x75",
                Date = DateTime.Now,
                Length = 1460,
                LengthOfFS = 0,
                LengthOfWaste = 100,
            },
            new DailyReportDataModel
            {
                Dimension = "25x75",
                Date = DateTime.Now,
                Length = 7120,
                LengthOfFS = 1250,
                LengthOfWaste = 2000,
            },
            new DailyReportDataModel
            {
                Dimension = "38x75",
                Date = DateTime.Now,
                Length = 2650,
                LengthOfFS = 145,
                LengthOfWaste = 10,
            },
            new DailyReportDataModel
            {
                Dimension = "19x125",
                Date = DateTime.Now.AddDays(-1),
                Length = 5102,
                LengthOfFS = 500,
                LengthOfWaste = 230,
            },
            new DailyReportDataModel
            {
                Dimension = "25x75",
                Date = DateTime.Now.AddDays(-1),
                Length = 1324,
                LengthOfFS = 0,
                LengthOfWaste = 420,
            },
            new DailyReportDataModel
            {
                Dimension = "38x75",
                Date = DateTime.Now.AddDays(-1),
                Length = 3124,
                LengthOfFS = 121,
                LengthOfWaste = 1230,
            },
            new DailyReportDataModel
            {
                Dimension = "25x75",
                Date = DateTime.Now.AddDays(-1),
                Length = 5000,
                LengthOfFS = 130,
                LengthOfWaste = 1123,
            },
            new DailyReportDataModel
            {
                Dimension = "25x75",
                Date = DateTime.Now.AddDays(-1),
                Length = 6301,
                LengthOfFS = 136,
                LengthOfWaste = 1200,
            },
            new DailyReportDataModel
            {
                Dimension = "38x75",
                Date = DateTime.Now.AddDays(-1),
                Length = 4122,
                LengthOfFS = 1323,
                LengthOfWaste = 0,
            },
            new DailyReportDataModel
            {
                Dimension = "19x125",
                Date = DateTime.Now.AddDays(-1),
                Length = 890,
                LengthOfFS = 214,
                LengthOfWaste = 100,
            },
            new DailyReportDataModel
            {
                Dimension = "19x125",
                Date = DateTime.Now.AddDays(-1),
                Length = 3251,
                LengthOfFS = 252,
                LengthOfWaste = 1235,
            },
            new DailyReportDataModel
            {
                Dimension = "25x75",
                Date = DateTime.Now.AddDays(-1),
                Length = 2135,
                LengthOfFS = 341,
                LengthOfWaste = 0,
            },
        };
    }
}
