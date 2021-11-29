using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Repositories.Prices
{
    public class GetByItemNumberMethodTests
    {
        [Fact]
        public async void GetByItemNumber_ShouldReturnData()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var model = new BundlePriceFromItemNumberViewModel
            {
                RawMaterialItemNumber = "51x25",
                VendorID = "10112423"
            };

            var expectedPrice = 7.34M;

            //Act
            var repo = new PricesRepository(dbContext);
            var actual = await repo.GetByItemNumber(model);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPrice, actual.Price);
        }

        [Fact]
        public async void GetByItemNumber_ShouldReturnDefault()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var model = new BundlePriceFromItemNumberViewModel
            {
                RawMaterialItemNumber = "51x25",
                VendorID = "123151"
            };

            //Act
            var repo = new PricesRepository(dbContext);
            var actual = await repo.GetByItemNumber(model);

            //Assert
            Assert.Null(actual);
        }
    }
}
