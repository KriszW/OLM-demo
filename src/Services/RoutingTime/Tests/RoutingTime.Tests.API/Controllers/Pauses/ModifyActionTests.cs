using Microsoft.AspNetCore.Mvc;
using OLM.Services.RoutingTime.API.Controllers;
using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Controllers.Pauses
{
    public class ModifyActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);
            var repo = new PausesRepository(dbContext);

            var id = 1;
            var model = new PauseModel()
            {
                ID = id,

            };

            //Act
            var controller = new PausesController(repo);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Theory]
        [MemberData(nameof(CreateBadRequestParameterData))]
        public async void ShouldReturnBadRequest(int id, PauseModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);
            var repo = new PausesRepository(dbContext);


            //Act
            var controller = new PausesController(repo);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateBadRequestParameterData() => new object[][]
        {
            new object[] { 1,new PauseModel() { ID = 0,} , "A model IDja és a parmatér ID nem egyezik" },
        };

        [Theory]
        [MemberData(nameof(CreateNotFoundParameterData))]
        public async void ShouldReturnNotFound(int id, PauseModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);
            var repo = new PausesRepository(dbContext);

            //Act
            var controller = new PausesController(repo);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateNotFoundParameterData() => new object[][]
        {
            new object[] { 100,new PauseModel() { ID = 100, } , "A 100 azonosítóval nem létezik szünet a rendszerben" },
        };
    }
}
