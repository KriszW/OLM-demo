using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.Prices.API.Controllers;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Controllers.BundlePriceManager
{
    public class AddActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var repo = new BundlePriceManagerRepository(dbContext);

            var model = new BundlePriceModel()
            {
                RawMaterialItemNumber = "24141x64231",
                Price = 513514M
            };

            //Act
            var controller = new BundlePriceManagerController(repo);
            var actionResult = await controller.Upload(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Theory]
        [MemberData(nameof(CreateConflictParameterData))]
        public async void ShouldReturnConflict(BundlePriceModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);
            var repo = new BundlePriceManagerRepository(dbContext);

            //Act
            var controller = new BundlePriceManagerController(repo);
            var actionResult = await controller.Upload(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<ConflictObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateConflictParameterData() => new object[][]
        {
            new object[] { new BundlePriceModel() { RawMaterialItemNumber = "5x25", Price = 120, VendorID = "10112423" } , "A '10112423' Vendorhoz már van feltöltve rakat ár a '5x25' cikkszámmal az adatbázisba" },
            new object[] { new BundlePriceModel() { ID = 1, RawMaterialItemNumber = "353x3512", Price = 120 } , "A 1 azonosítóval már létezik rakat ár a rendszerben" },
        };
    }
}
