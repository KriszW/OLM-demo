using Microsoft.AspNetCore.Mvc.ApplicationParts;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Repositories.RawBundleIDPrice
{
    public class GetByBundleIDsMethodTests
    {
        [Fact]
        public async void GetByBundleIDs_ShouldReturnAllDataExceptDoubleBundleIDForDimension()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);

            var model = new List<string>
            {
                "bundle1",
                "bundle2",
            };

            var expectedCount = 2;

            //Act
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);
            var actual = await repo.GetByBundleIDs(model);

            //Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expectedCount, actual.Count());

            foreach (var item in actual)
            {
                Assert.False(item.ItemNumber == default);
                Assert.False(item.Price == default);
            }
        }

        [Fact]
        public async void GetByBundleIDs_ShouldReturnAllData()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);

            var model = new List<string>
            {
                "bundle1",
                "bundle2",
                "bundle4",
            };

            var expectedCount = 2;

            //Act
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);
            var actual = await repo.GetByBundleIDs(model);

            //Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expectedCount, actual.Count());

            foreach (var item in actual)
            {
                Assert.False(item.ItemNumber == default);
                Assert.False(item.Price == default);
            }
        }

        [Fact]
        public async void GetByBundleIDs_ShouldReturnOneLessBecauseItIsNotUploadedToDB()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);

            var model = new List<string>
            {
                "bundle1",
                "bundle1000"
            };

            var expectedCount = 1;

            //Act
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);
            var actual = await repo.GetByBundleIDs(model);

            //Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expectedCount, actual.Count());

            foreach (var item in actual)
            {
                Assert.False(item.ItemNumber == default);
                Assert.False(item.Price == default);
            }
        }

        [Fact]
        public async void GetByBundleIDs_ShouldReturnDefaultForModelIsNull()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);

            var model = default(IEnumerable<string>);

            //Act
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);
            var actual = await repo.GetByBundleIDs(model);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async void GetByBundleIDs_ShouldReturnDefaultForModelIsEmpty()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);

            var model = new List<string>();

            //Act
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);
            var actual = await repo.GetByBundleIDs(model);

            //Assert
            Assert.Null(actual);
        }
    }
}
