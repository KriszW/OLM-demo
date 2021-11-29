using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Services.TCOBundleAggregator
{
    public class AggregateForBundleMethodTests
    {
        [Theory]
        [MemberData(nameof(CreateTestParameters))]
        public async void Aggregate(string bundleID, BundleTCOAPIResponseViewModel expectedData, bool expectedToBeNull)
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{bundleID}-{expectedData.CalculatedValue}-{expectedData.ExpectedTCOValue}-{expectedData.MaximumDifference}-{expectedToBeNull}");
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoProviderService = new EuroTCOCalculationProvider();
            var tcoCalculatorService = new EuroTCOCalculatorService(tcoDataRepo, tcoProviderService);
            var tcoSettingsRepo = new TCOSettingsRepository(dbContext);

            //Act
            var service = new BundleTCOAggregatorService(dbContext,tcoDataRepo,tcoCalculatorService, tcoSettingsRepo);
            var actual = await service.AggregateDataForBundle(bundleID);

            //Assert
            if (expectedToBeNull == false)
            {
                Assert.NotNull(actual);
                Assert.Equal(expectedData.CalculatedValue, actual.CalculatedValue);
                Assert.Equal(expectedData.ExpectedTCOValue, actual.ExpectedTCOValue);
                Assert.Equal(expectedData.MaximumDifference, actual.MaximumDifference);
            }
            else
            {
                Assert.Null(actual);
            }
        }

        public static object[][] CreateTestParameters() => new object[][]
        {
            new object[] { "bundle1", new BundleTCOAPIResponseViewModel(2.6904, 1.2, 0.1), false },
            new object[] { "bundle3", new BundleTCOAPIResponseViewModel(17.20484962406015, 4.3, 0.4), false },
            new object[] { "bundle100", new BundleTCOAPIResponseViewModel(0,0,0), true },
        };
    }
}
