using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Controllers;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses.OneMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Controllers.Bundle
{
    public class GetWeeklyBundlesActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();
            var repo = new BundleRepository(dbContext, dateProvider);

            var machineName = "0";
            var expectedCount = 2;
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                expectedCount--;
            }

            //Act
            var controller = new BundleController(repo);
            var actionResult = await controller.GetWeeklyBundles(machineName);
            var actualData = await repo.GetWeeklyBundlesForMachine(machineName);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundleWeeklyAPIResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundleWeeklyAPIResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.Equal(actualData.Count, actual.Model.Bundles.Count());
            Assert.Equal(expectedCount, actual.Model.Bundles.Count());
            Assert.True(actual.Model.Bundles.All(b => b.MachineName == machineName));
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();
            var repo = new BundleRepository(dbContext, dateProvider);

            var machineName = "5";
            var expectedMSG = $"A '{machineName}' géphez nincs feltöltve a heti rakatok adatai";

            //Act
            var controller = new BundleController(repo);
            var actionResult = await controller.GetWeeklyBundles(machineName);
            var actualData = await repo.GetWeeklyBundlesForMachine(machineName);

            //Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actualData);
            Assert.Empty(actualData);

            Assert.IsType<ActionResult<APIResponse<BundleWeeklyAPIResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundleWeeklyAPIResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
