using Microsoft.AspNetCore.Mvc;
using OLM.Services.Routing.API.Controllers;
using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Controllers.RoutingManager
{
    public class AddActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);
            var repo = new RoutingManagerRepository(dbContext);

            var model = new RoutingModel()
            {
            };

            //Act
            var controller = new RoutingManagerController(repo);
            var actionResult = await controller.Add(model);

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
        public async void ShouldReturnConflict(RoutingModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{expectedMSG}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);
            var repo = new RoutingManagerRepository(dbContext);

            //Act
            var controller = new RoutingManagerController(repo);
            var actionResult = await controller.Add(model);

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
            new object[] { new RoutingModel() { ID = 1, } , "A 1 azonosítóval már van routing idő feltöltve a rendszerben" },
        };
    }
}
