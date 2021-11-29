using Microsoft.EntityFrameworkCore;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Tests.FakeImplementations
{
    public static class FakeTCODbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<TCODataDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-tco-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new TCODataDbContext(options);

            await dbContext.AddRangeAsync(CreateTCODataModels());
            await dbContext.AddRangeAsync(CreateContansSettings());
            await dbContext.SaveChangesAsync();
        }

        public static List<TCOValueSettingsModel> CreateContansSettings() => new List<TCOValueSettingsModel>
        {
            new TCOValueSettingsModel()
            {
                RawMaterialItemNumber = "5x25",
                ExpectedTCOValue = 1.2,
                MaximumDifference = 0.1
            }
            ,new TCOValueSettingsModel()
            {
                RawMaterialItemNumber = "51x25",
                ExpectedTCOValue = 4.3,
                MaximumDifference = 0.4
            }
            ,new TCOValueSettingsModel()
            {
                RawMaterialItemNumber = "13x25",
                ExpectedTCOValue = 3.2,
                MaximumDifference = 0.7
            }
            ,new TCOValueSettingsModel()
            {
                RawMaterialItemNumber = "83x25",
                ExpectedTCOValue = 10.4,
                MaximumDifference = 0.2
            }
        };

        public static List<TCODataModel> CreateTCODataModels() => new List<TCODataModel>
        {
            new TCODataModel()
            {
                BundleID = "bundle1",
                RawMaterialItemNumber = "5x25",
                Primary = 3000,
                Secondary = 4000,
                Volume = 5320,
                VendorID = "102340"
            },
            new TCODataModel()
            {
                BundleID = "bundle2",
                RawMaterialItemNumber = "5x25",
                Primary = 5414,
                Secondary = 1305,
                Volume = 5462,
                VendorID = "103341"
            },
            new TCODataModel()
            {
                BundleID = "bundle3",
                RawMaterialItemNumber = "51x25",
                Primary = 132,
                Secondary = 400,
                Volume = 1247,

            }
            ,new TCODataModel()
            {
                BundleID = "bundle4",
                RawMaterialItemNumber = "51x25",
                Primary = 4110,
                Secondary = 5462,
                Volume = 7531,

            }
            ,new TCODataModel()
            {
                BundleID = "bundle5",
                RawMaterialItemNumber = "13x25",
                Primary = 2432,
                Secondary = 2310,
                Volume = 4321,
            }
        };
    }
}
