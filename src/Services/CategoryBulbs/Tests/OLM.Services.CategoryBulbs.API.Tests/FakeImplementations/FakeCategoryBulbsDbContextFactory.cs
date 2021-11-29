using Microsoft.EntityFrameworkCore;
using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Tests.FakeImplementations
{
    public static class FakeCategoryBulbsDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<CategoryBulbsDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-categorybulbs-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new CategoryBulbsDbContext(options);

            await dbContext.AddRangeAsync(CreateModels());
            await dbContext.AddRangeAsync(CreateCategories());
            await dbContext.SaveChangesAsync();
        }

        public static List<ItemnumberCategoryModel> CreateCategories() => new List<ItemnumberCategoryModel>
        {
            new ItemnumberCategoryModel
            {
                CategoryType = "1",
                Itemnumber = "10113556MK"
            },
            new ItemnumberCategoryModel
            {
                CategoryType = "4",
                Itemnumber = "10153556CK"
            },
            new ItemnumberCategoryModel
            {
                CategoryType = "3",
                Itemnumber = "10113500AD"
            },
            new ItemnumberCategoryModel
            {
                CategoryType = "2",
                Itemnumber = "10153556JK"
            },
            new ItemnumberCategoryModel
            {
                CategoryType = "2",
                Itemnumber = "10646556MK"
            },
            new ItemnumberCategoryModel
            {
                CategoryType = "1",
                Itemnumber = "106313556MK"
            },
            new ItemnumberCategoryModel
            {
                CategoryType = "1",
                Itemnumber = "10153500CK"
            }
        };

        public static List<BundleItemnumbersModel> CreateModels() => new List<BundleItemnumbersModel>()
        {
            new BundleItemnumbersModel
            {
                BundleID = "bundle1",
                Models = new List<ItemnumberModel>
                {
                    new ItemnumberModel
                    {
                        Itemnumber = "10113556MK"
                    },
                    new ItemnumberModel
                    {
                        Itemnumber = "10153556CK"
                    },
                    new ItemnumberModel
                    {
                        Itemnumber = "10113500AD"
                    }
                }
            },
            new BundleItemnumbersModel
            {
                BundleID = "bundle2",
                Models = new List<ItemnumberModel>
                {
                    new ItemnumberModel
                    {
                        Itemnumber = "10646556MK"
                    },
                    new ItemnumberModel
                    {
                        Itemnumber = "10153556JK"
                    },
                    new ItemnumberModel
                    {
                        Itemnumber = "10170500AD"
                    }
                }
            },
            new BundleItemnumbersModel
            {
                BundleID = "bundle3",
                Models = new List<ItemnumberModel>
                {
                    new ItemnumberModel
                    {
                        Itemnumber = "106313556MK"
                    },
                    new ItemnumberModel
                    {
                        Itemnumber = "10153500CK"
                    },
                    new ItemnumberModel
                    {
                        Itemnumber = "10113500AD"
                    }
                }
            }
        };
    }
}
