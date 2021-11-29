using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Services.TCO.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Services.TCOBundleCalculator
{
    public class CalculateTCOMethodTests
    {
        [Theory]
        [InlineData("", 0.0)]
        [InlineData("bundle1", 2.6904)]
        [InlineData("bundle100", 0.0)]
        public async void Calc(string bundleID, double expectedReturnValue)
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{bundleID}-{expectedReturnValue}");
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoProviderService = new EuroTCOCalculationProvider();
            var service = new EuroTCOCalculatorService(tcoDataRepo, tcoProviderService);

            //Act
            var actual = await service.CalculateTCO(bundleID);

            //Assert
            Assert.Equal(expectedReturnValue, actual);
        }

        public async void CalcShouldFail()
        {

        }
    }
}
