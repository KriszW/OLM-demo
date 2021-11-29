using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.Prices.API.Controllers;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Controllers.BundleIDPrice
{
    public class GetForManyActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);

            var model = new List<string> { "bundle1", "bundle2" };

            //Act
            var controller = new BundleIDPriceController(repo);
            var actionResult = await controller.GetForMany(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<IEnumerable<BundlePriceWithBundleIDsViewModel>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<BundlePriceWithBundleIDsViewModel>>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
            Assert.NotEmpty(actual.Model);
            Assert.Equal(model.Count, actual.Model.Count());
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var priceRepo = new PricesRepository(dbContext);
            var repo = new RawBundleIDPriceRepository(dbContext, priceRepo);

            var model = new List<string> { };
            var expectedMSG = $"A megadatt adatokhoz nem található rakatár az adatbázisba";

            //Act
            var controller = new BundleIDPriceController(repo);
            var actionResult = await controller.GetForMany(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<IEnumerable<BundlePriceWithBundleIDsViewModel>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<BundlePriceWithBundleIDsViewModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
            Assert.Null(actual.Model);
        }
    }
}
