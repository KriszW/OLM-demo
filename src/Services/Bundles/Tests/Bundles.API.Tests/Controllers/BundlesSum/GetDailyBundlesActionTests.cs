using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Controllers;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using OLM.Services.Bundles.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses.Summarized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Controllers.BundlesSum
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
            var repo = new BundlesSumRepository(dbContext, dateProvider);

            var expectedCount = 2;

            //Act
            var controller = new BundlesSumerizedController(repo);
            var actionResult = await controller.GetDailyBundles();
            var actualData = await repo.GetDailySumBundles();

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<DailySummarizedBundlesAPIResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<DailySummarizedBundlesAPIResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.Equal(actualData.Count, actual.Model.Bundles.Count());
            Assert.Equal(expectedCount, actual.Model.Bundles.Count());
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeBundlesDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlesDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlesDbContext(dbOptions);
            var dateProvider = new DebugStartDateProvider();
            var repo = new BundlesSumRepository(dbContext, dateProvider);

            var expectedMSG = $"Egy géphez sincs feltöltve a mai naphoz rakatadat";

            //Act
            await FakeBundlesDbContextFactory.DeleteAll(dbOptions);
            var controller = new BundlesSumerizedController(repo);
            var actionResult = await controller.GetDailyBundles();
            var actualData = await repo.GetDailySumBundles();

            //Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actualData);
            Assert.Empty(actualData);

            Assert.IsType<ActionResult<APIResponse<DailySummarizedBundlesAPIResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<DailySummarizedBundlesAPIResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
