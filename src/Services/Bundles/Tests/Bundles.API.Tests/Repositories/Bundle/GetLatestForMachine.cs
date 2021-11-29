using System;
using System.Collections.Generic;
using System.Text;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Repositories.Bundle
{
    public class GetLatestForMachine
    {
        [Theory]
        [InlineData("0", false)]
        [InlineData("2", false)]
        [InlineData("1", true)]
        [InlineData("5", true)]
        public async void FetchData(string machineName, bool expectedToBeNull)
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"'{machineName}'-{expectedToBeNull}");
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();

            //Act
            var repo = new BundleRepository(dbContext, dateProvider);
            var actual = await repo.GetLatestForMachine(machineName);

            //Assert
            Assert.Equal(expectedToBeNull, actual == default);
        }
    }
}
