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

namespace OLM.Services.Bundles.Prices.Tests.API.Controllers.BundlePrices
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
            var repo = new PricesRepository(dbContext);

            var model = new List<BundlePriceFromItemNumberViewModel> 
            {
                new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "51x25",
                    VendorID = "10112323",
                },
                new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "51x25",
                    VendorID = "10112423",
                },
                new BundlePriceFromItemNumberViewModel
                {
                    RawMaterialItemNumber = "83x25",
                    VendorID = "10112323",
                },
            };

            //Act
            var controller = new BundlePricesController(repo);
            var actionResult = await controller.GetForMany(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<IEnumerable<BundlePriceViewModel>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<BundlePriceViewModel>>>(requestObject.Value);
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
            var repo = new PricesRepository(dbContext);

            var model = new List<BundlePriceFromItemNumberViewModel> { };
            var expectedMSG = $"A megadatt adatokhoz nem található rakatár az adatbázisba";

            //Act
            var controller = new BundlePricesController(repo);
            var actionResult = await controller.GetForMany(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<IEnumerable<BundlePriceViewModel>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<BundlePriceViewModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
            Assert.Null(actual.Model);
        }
    }
}
