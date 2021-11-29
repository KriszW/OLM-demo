using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.TCO.API.Controllers;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Controllers.RawTCOCalculator
{
    public class CalcForManyActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoSettingsRepo = new TCOSettingsRepository(dbContext);
            var tcoCalcProviderService = new EuroTCOCalculationProvider();
            var tcoCalcService = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProviderService);
            var tcoSettingsService = new RawTCOBundleSettingsAggregatorService();
            var service = new RawTCOAggregatorService(tcoDataRepo, tcoSettingsRepo, tcoCalcService, tcoSettingsService);

            var model = new List<RawTCOQueryDataViewModel>
            {
                new RawTCOQueryDataViewModel
                {
                    BundleIDs = new List<string>
                    {
                        "bundle1"
                    },
                    ItemNumber = "5x25",
                    VendorID = "102340",
                    Price = 60000
                },new RawTCOQueryDataViewModel
                {
                    BundleIDs = new List<string>
                    {
                        "bundle2"
                    },
                    ItemNumber = "51x35",
                    VendorID = "103341",
                    Price = 100000
                },

            };
            var expected = 22499.72;

            //Act
            var controller = new RawTCOCalculatorController(service);
            var actionResult = await controller.CalcForMany(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundleTCOAPIResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundleTCOAPIResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.Equal(expected, actual.Model.CalculatedValue);
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoSettingsRepo = new TCOSettingsRepository(dbContext);
            var tcoCalcProviderService = new EuroTCOCalculationProvider();
            var tcoCalcService = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProviderService);
            var tcoSettingsService = new RawTCOBundleSettingsAggregatorService();
            var service = new RawTCOAggregatorService(tcoDataRepo, tcoSettingsRepo, tcoCalcService, tcoSettingsService);

            var model = default(IEnumerable<RawTCOQueryDataViewModel>);
            var expectedMSG = $"A megadott cikkekhez és beszállítókhoz nem sikerült kiszámolni a TCO értéket";

            //Act
            var controller = new RawTCOCalculatorController(service);
            var actionResult = await controller.CalcForMany(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundleTCOAPIResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundleTCOAPIResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
