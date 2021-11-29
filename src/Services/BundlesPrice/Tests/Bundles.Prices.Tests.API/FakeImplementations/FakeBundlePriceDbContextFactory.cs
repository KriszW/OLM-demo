using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.Tests.API.FakeImplementations
{
    public static class FakeBundlePriceDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<BundlePriceDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-tco-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new BundlePriceDbContext(options);

            await dbContext.AddRangeAsync(CreateModels());
            await dbContext.AddRangeAsync(CreateBundles());
            await dbContext.SaveChangesAsync();
        }

        public static List<RawBundlesModel> CreateBundles() => new List<RawBundlesModel>
        {
            new RawBundlesModel
            {
                BundleID = "bundle1",
                ItemNumber = "5x25",
                VendorID = "10112423"
            },
            new RawBundlesModel
            {
                BundleID = "bundle2",
                ItemNumber = "51x25",
                VendorID = "10112323"
            },
            new RawBundlesModel
            {
                BundleID = "bundle3",
                ItemNumber = "83x25",
                VendorID = "10112323"
            },
            new RawBundlesModel
            {
                BundleID = "bundle4",
                ItemNumber = "5x25",
                VendorID = "10112423"
            },
        };

        public static List<BundlePriceModel> CreateModels() => new List<BundlePriceModel>
        {
            new BundlePriceModel()
            {
                RawMaterialItemNumber = "5x25",
                Price = 3.54M,
                VendorID = "10112423",
            }
            ,new BundlePriceModel()
            {
                RawMaterialItemNumber = "51x25",
                Price = 7.34M,
                VendorID = "10112323",
            }            
            ,new BundlePriceModel()
            {
                RawMaterialItemNumber = "51x25",
                Price = 7.34M,
                VendorID = "10112423",
            }
            ,new BundlePriceModel()
            {
                RawMaterialItemNumber = "13x25",
                Price = 5.14M,
                VendorID = "10112323",
            }
            ,new BundlePriceModel()
            {
                RawMaterialItemNumber = "83x25",
                Price = 10.94M,
                VendorID = "10112323",
            }
        };

    }
}
