using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Repositories.Prices
{
    public class GetByItemNumbersMethodTests
    {
        [Fact]
        public async void GetByItemNumbers_ShouldReturnAllData()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var model = new List<BundlePriceFromItemNumberViewModel>
            {
                new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "51x25",
                    VendorID = "10112423"
                }
                ,new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "13x25",
                    VendorID = "10112323"
                }
                ,new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "83x25",
                    VendorID = "10112323"
                },
            };

            var expectedCount = 3;

            //Act
            var repo = new PricesRepository(dbContext);
            var actual = await repo.GetByItemNumbers(model);

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
        public async void GetByItemNumbers_ShouldReturnOneLessBecauseItIsNotUploadedToDB()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var model = new List<BundlePriceFromItemNumberViewModel>
            {
                new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "51x25",
                    VendorID = "10112423"
                }
                ,new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "13x25",
                    VendorID = "10112323"
                }
                ,new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "83x25",
                    VendorID = "10112523"
                },
            };

            var expectedCount = 2;

            //Act
            var repo = new PricesRepository(dbContext);
            var actual = await repo.GetByItemNumbers(model);

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
    }
}
