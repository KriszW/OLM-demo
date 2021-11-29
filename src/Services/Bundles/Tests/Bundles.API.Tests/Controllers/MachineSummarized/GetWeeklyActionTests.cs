using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Controllers;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Machine;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Controllers.MachineSummarized
{
    public class GetWeeklyActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();
            var repo = new MachineBundlesRepository(dbContext, dateProvider);

            var machineName = "0";

            //Act
            var controller = new MachineSummarizedDataController(repo);
            var actionResult = await controller.GetWeekly(machineName);
            var actualData = await repo.GetWeeklySummarizedData(machineName);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<SummarizedBundlesForMachineDataViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actualResponse = Assert.IsAssignableFrom<APIResponse<SummarizedBundlesForMachineDataViewModel>>(requestObject.Value);
            Assert.True(actualResponse.Success);
            var actual = Assert.IsAssignableFrom<SummarizedBundlesForMachineDataViewModel>(actualResponse.Model);
            Assert.Equal(actualData.AllInput, actual.AllInput);
            Assert.Equal(actualData.AllProduced, actual.AllProduced);
            Assert.Equal(actualData.AllFS, actual.AllFS);
            Assert.Equal(actualData.AVGWastePercentage, actual.AVGWastePercentage);
            Assert.Equal(machineName, actual.MachineName);
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();
            var repo = new MachineBundlesRepository(dbContext, dateProvider);

            var machineName = "5";
            var expectedMSG = $"A '{machineName}' géphez nincs feltöltve a heti rakatok adatai";

            //Act
            var controller = new MachineSummarizedDataController(repo);
            var actionResult = await controller.GetWeekly(machineName);
            var actualData = await repo.GetWeeklySummarizedData(machineName);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Null(actualData);

            Assert.IsType<ActionResult<APIResponse<SummarizedBundlesForMachineDataViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<SummarizedBundlesForMachineDataViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
