using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Controllers;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Machine;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Controllers.MachinesSummarized
{
    public class GetDailyActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();
            var repo = new MachineSumRepository(dbContext, dateProvider);

            //Act
            var controller = new MachinesSummarizedDataController(repo);
            var actionResult = await controller.GetDaily();
            var actualData = await repo.GetAllDailyBundles();

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<SummarizedDataForMachinesViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actualResponse = Assert.IsAssignableFrom<APIResponse<SummarizedDataForMachinesViewModel>>(requestObject.Value);
            Assert.True(actualResponse.Success);
            var actual = Assert.IsAssignableFrom<SummarizedDataForMachinesViewModel>(actualResponse.Model);
            Assert.Equal(actualData.AllInput, actual.AllInput);
            Assert.Equal(actualData.AllProduced, actual.AllProduced);
            Assert.Equal(actualData.AllFS, actual.AllFS);
            Assert.Equal(actualData.AVGWastePercentage, actual.AVGWastePercentage);
        }

        
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();
            var repo = new MachineSumRepository(dbContext, dateProvider);

            var expectedMSG = $"Egy géphez sincs feltöltve a mai naphoz rakatadat";

            //Act
            var controller = new MachinesSummarizedDataController(repo);
            var actionResult = await controller.GetDaily();
            var actualData = await repo.GetAllDailyBundles();

            //Assert
            Assert.NotNull(actionResult);
            Assert.Null(actualData);

            Assert.IsType<ActionResult<APIResponse<SummarizedDataForMachinesViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<SummarizedDataForMachinesViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
