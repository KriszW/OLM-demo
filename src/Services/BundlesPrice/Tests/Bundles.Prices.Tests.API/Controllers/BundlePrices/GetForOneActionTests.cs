using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.Prices.API.Controllers;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Controllers.BundlePrices
{
    public class GetForOneActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var repo = new PricesRepository(dbContext);

            var model = new BundlePriceFromItemNumberViewModel
            {
                RawMaterialItemNumber = "51x25",
                VendorID = "10112423",
            };

            var expectedPrice = 7.34M;

            //Act
            var controller = new BundlePricesController(repo);
            var actionResult = await controller.GetForOne(model.RawMaterialItemNumber, model.VendorID);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundlePriceViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundlePriceViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
            Assert.Equal(expectedPrice, actual.Model.Price);
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var repo = new PricesRepository(dbContext);

            var model = new BundlePriceFromItemNumberViewModel
            {
                RawMaterialItemNumber = "41541x60103",
                VendorID = "3415u781",
            };
            var expectedMSG = $"A '{model.VendorID}' Vendorhoz már van feltöltve rakat ár a '{model.RawMaterialItemNumber}' cikkszámmal az adatbázisba";

            //Act
            var controller = new BundlePricesController(repo);
            var actionResult = await controller.GetForOne(model.RawMaterialItemNumber, model.VendorID);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<BundlePriceViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<BundlePriceViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
