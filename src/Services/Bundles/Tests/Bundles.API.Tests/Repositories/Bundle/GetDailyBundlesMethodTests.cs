using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Repositories.Bundle
{
    public class GetDailyBundlesMethodTests
    {
        [Theory]
        [InlineData("0", 1)]
        [InlineData("2", 1)]
        [InlineData("1", 0)]
        public async void FetchData(string machineName, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"'{machineName}'-{expectedCount}");
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();

            //Act
            var repo = new BundleRepository(dbContext, dateProvider);
            var actual = await repo.GetDailyBundlesForMachine(machineName);

            //Assert
            Assert.Equal(expectedCount, actual.Count);
        }
    }
}
