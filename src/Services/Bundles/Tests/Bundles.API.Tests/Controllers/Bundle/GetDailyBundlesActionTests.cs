using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using OLM.Services.Bundles.API.Controllers;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Shared.Models.Bundles.APIResponses.OneMachine;

namespace OLM.Services.Bundles.API.Tests.Controllers.Bundle
{
    public class GetDailyBundlesActionTests
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
            var expectedCount = 1;

            //Act
            var controller = new BundleController(repo);
            var actionResult = await controller.GetDailyBundles(machineName);
            var actualData = await repo.GetDailyBundlesForMachine(machineName);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundleDailyAPIResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundleDailyAPIResponseViewModel>>(requestObject.Value);
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
            var expectedMSG = $"A '{machineName}' géphez nincs feltöltve a napi rakatok adatai";

            //Act
            var controller = new BundleController(repo);
            var actionResult = await controller.GetDailyBundles(machineName);
            var actualData = await repo.GetDailyBundlesForMachine(machineName);

            //Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actualData);
            Assert.Empty(actualData);

            Assert.IsType<ActionResult<APIResponse<BundleDailyAPIResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundleDailyAPIResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
