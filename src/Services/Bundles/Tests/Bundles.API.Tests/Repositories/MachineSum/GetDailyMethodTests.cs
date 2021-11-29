using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Machine;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Repositories.MachineSum
{
    public class GetDailyMethodTests
    {
        [Fact]
        public async void FetchData()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();

            var bundlesRepo = new BundlesSumRepository(dbContext, dateProvider);
            var expectedBundlesCount = 2;

            //Act
            var repo = new MachineSumRepository(dbContext, dateProvider);
            var actual = await repo.GetAllDailyBundles();
            var expectedBundles = await bundlesRepo.GetDailySumBundles();

            //Assert
            Assert.Equal(expectedBundlesCount, expectedBundles.Count);
            Assert.Equal(expectedBundles.Sum(b => b.Input), actual.AllInput);
            Assert.Equal(expectedBundles.Sum(b => b.Produced), actual.AllProduced);
            Assert.Equal(expectedBundles.Sum(b => b.FS), actual.AllFS);
            Assert.Equal(expectedBundles.Average(b => b.CalculateWastePercentage()), actual.AVGWastePercentage);
        }
    }
}
