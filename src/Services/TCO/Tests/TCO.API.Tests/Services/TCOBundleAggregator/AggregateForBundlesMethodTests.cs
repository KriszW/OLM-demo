using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Services.TCO.API.Tests.TestExtensions;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Services.TCOBundleAggregator
{
    public class AggregateForBundlesMethodTests
    {
        [Theory]
        [MemberData(nameof(CreateTestParameters))]
        public async void Aggregate(IEnumerable<string> bundleIDs, BundleTCOAPIResponseViewModel expectedData, bool expectedToBeNull)
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{bundleIDs.ConvertDataIntoOneString()}-{expectedData.CalculatedValue}-{expectedData.ExpectedTCOValue}-{expectedData.MaximumDifference}-{expectedToBeNull}");
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoProviderService = new EuroTCOCalculationProvider();
            var tcoCalculatorService = new EuroTCOCalculatorService(tcoDataRepo, tcoProviderService);
            var tcoSettingsRepo = new TCOSettingsRepository(dbContext);

            //Act
            var service = new BundleTCOAggregatorService(dbContext, tcoDataRepo, tcoCalculatorService, tcoSettingsRepo);
            var actual = await service.AggregateDataForBundles(bundleIDs);

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
            new object[] { new string[] { "bundle1" }, new BundleTCOAPIResponseViewModel(2.6904,1.2,0.1), false },
            new object[] { new string[] { "bundle1", "bundle2" }, new BundleTCOAPIResponseViewModel(2.782147386835775, 1.2,0.1), false },
            new object[] { new string[] { "bundle1", "bundle3" }, new BundleTCOAPIResponseViewModel(9.947624812030075, 2.75,0.25), false },
            new object[] { new string[] { "bundle100" }, new BundleTCOAPIResponseViewModel(0,0,0), true },
        };
    }
}
