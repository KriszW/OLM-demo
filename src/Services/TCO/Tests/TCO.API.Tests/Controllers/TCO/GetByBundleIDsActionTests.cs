using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.TCO.API.Controllers;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Controllers.TCO
{
    public class GetByBundleIDsActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoProviderService = new EuroTCOCalculationProvider();
            var service = new EuroTCOCalculatorService(tcoDataRepo, tcoProviderService);

            var bundleIDs = new string[] { "bundle1", "bundle2" };
            var expected = 2.78;

            //Act
            var controller = new TCOController(service);
            var actionResult = await controller.CalculateAVGTCO(bundleIDs);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<double>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<double>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.Equal(expected, actual.Model);
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoProviderService = new EuroTCOCalculationProvider();
            var service = new EuroTCOCalculatorService(tcoDataRepo, tcoProviderService);

            var bundleIDs = new string[] { "bundle100" };
            var expectedMSG = $"A rakatokhoz nem sikerült kiszámolni a TCO átlag értékét";

            //Act
            var controller = new TCOController(service);
            var actionResult = await controller.CalculateAVGTCO(bundleIDs);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<double>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<double>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
