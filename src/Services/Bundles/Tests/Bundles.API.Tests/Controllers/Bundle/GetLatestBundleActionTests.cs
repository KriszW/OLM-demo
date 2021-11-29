using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Controllers;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Models;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Controllers.Bundle
{
    public class GetLatestBundleActionTests
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

            var machineName = "2";

            //Act
            var controller = new BundleController(repo);
            var actionResult = await controller.GetLatestBundle(machineName);
            var actualData = await repo.GetLatestForMachine(machineName);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundleAPIResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundleAPIResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.Equal(machineName, actual.Model.MachineName);
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
            var expectedMSG = $"A '{machineName}' géphez nincs feltöltve a legutolsó rakat";

            //Act
            var controller = new BundleController(repo);
            var actionResult = await controller.GetLatestBundle(machineName);
            var actualData = await repo.GetLatestForMachine(machineName);

            //Assert
            Assert.NotNull(actionResult);
            Assert.Null(actualData);

            Assert.IsType<ActionResult<APIResponse<BundleAPIResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundleAPIResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
