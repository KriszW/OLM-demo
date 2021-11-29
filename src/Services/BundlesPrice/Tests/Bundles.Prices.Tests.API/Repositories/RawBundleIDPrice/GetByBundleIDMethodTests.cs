using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Repositories.RawBundleIDPrice
{
    public class GetByBundleIDMethodTests
    {
        [Fact]
        public async void GetByBundleID_ShouldReturnData()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);

            var bundleID = "bundle1";

            var expectedPrice = 3.54M;

            //Act
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);
            var actual = await repo.GetByBundleID(bundleID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPrice, actual.Price);
        }

        [Fact]
        public async void GetByBundleID_ShouldReturnDefaultForBundleIDIsNull()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);

            var bundleID = default(string);

            //Act
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);
            var actual = await repo.GetByBundleID(bundleID);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async void GetByBundleID_ShouldReturnDefaultForNotExistingBundleID()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);

            var bundleID = "bundle1000";

            //Act
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);
            var actual = await repo.GetByBundleID(bundleID);

            //Assert
            Assert.Null(actual);
        }
    }
}
