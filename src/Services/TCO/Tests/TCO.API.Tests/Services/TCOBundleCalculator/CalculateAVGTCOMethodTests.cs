using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Services.TCO.API.Tests.TestExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Services.TCOBundleCalculator
{
    public class CalculateAVGTCOMethodTests
    {
        [Theory]
        [InlineData(new string[] { }, 0.0)]
        [InlineData(new string[] { "bundle1" }, 2.6904)]
        [InlineData(new string[] { "bundle1", "bundle2" }, 2.782147386835775)]
        [InlineData(new string[] { "bundle100" }, 0.0)]
        public async void Calc(IEnumerable<string> bundleIDs, double expectedReturnValue)
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{bundleIDs.ConvertDataIntoOneString()}{expectedReturnValue}");
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoProviderService = new EuroTCOCalculationProvider();
            var service = new EuroTCOCalculatorService(tcoDataRepo, tcoProviderService);
            
            //Act
            var actual = await service.CalculateAVGTCO(bundleIDs);

            //Assert
            Assert.Equal(expectedReturnValue, actual);
        }
    }
}
