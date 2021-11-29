using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Repositories.BundlesSum
{
    public class GetDailySummirezedBundlesMethodTests
    {
        [Fact]
        public async void FetchData()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();

            var expectedCount = 2;

            //Act
            var repo = new BundlesSumRepository(dbContext, dateProvider);
            var actual = await repo.GetDailySumBundles();

            //Assert
            Assert.Equal(expectedCount, actual.Count);
        }
    }
}
