using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Repositories.BundlePriceManager
{
    public class GetByIDMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(10, true)]
        [InlineData(0, true)]
        [InlineData(-1, true)]
        public async void GetByID_ShouldReturnSuccess(int id, bool isNull)
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{isNull}");
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.Equal(isNull, actual == default);
        }
    }
}
