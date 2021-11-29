using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.Prices.API.Controllers;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Controllers.BundlePriceManager
{
    public class GetByIDActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var repo = new BundlePriceManagerRepository(dbContext);

            var id = 1;

            //Act
            var controller = new BundlePriceManagerController(repo);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundlePriceModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundlePriceModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var repo = new BundlePriceManagerRepository(dbContext);

            var id = 100;
            var expectedMSG = $"A {id} azonosítóval nincs feltöltve ár az adatbázisba";

            //Act
            var controller = new BundlePriceManagerController(repo);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundlePriceModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundlePriceModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
