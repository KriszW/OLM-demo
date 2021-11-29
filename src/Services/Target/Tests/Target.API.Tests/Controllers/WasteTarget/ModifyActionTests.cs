using Microsoft.AspNetCore.Mvc;
using OLM.Services.Target.API.Controllers;
using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Models;
using OLM.Services.Target.API.Services.Repositories.Implementations;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OLM.Services.Target.API.Tests.FakeImplementations;

namespace OLM.Services.Target.API.Tests.Controllers.WasteTarget
{
    public class ModifyActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);
            var repo = new WasteTargetRepository(dbContext);

            var id = 1;
            var model = new WasteTargetDataModel()
            {
                ID = id,
                Dimension = "24141x64231",
                Target = 0.23
            };

            //Act
            var controller = new WasteTargetController(repo);
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
        public async void ShouldReturnBadRequest(int id, WasteTargetDataModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);
            var repo = new WasteTargetRepository(dbContext);


            //Act
            var controller = new WasteTargetController(repo);
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
            new object[] { 1,new WasteTargetDataModel() { ID = 0, Dimension = "353x3512", Target = 0 } , "A model IDja és a parmatér ID nem egyezik" },
        };

        [Theory]
        [MemberData(nameof(CreateConflictParameterData))]
        public async void ShouldReturnConflict(int id, WasteTargetDataModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);
            var repo = new WasteTargetRepository(dbContext);

            //Act
            var controller = new WasteTargetController(repo);
            var actionResult = await controller.Modify(id, model);

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
           new object[] { 1,new WasteTargetDataModel() { ID = 1, Dimension = "13x25", Target = 0 } , "A '13x25' Target Dimenzióval már van feltöltve Selejt Target Dimenzió az adatbázisba" },
        };

        [Theory]
        [MemberData(nameof(CreateNotFoundParameterData))]
        public async void ShouldReturnNotFound(int id, WasteTargetDataModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);
            var repo = new WasteTargetRepository(dbContext);

            //Act
            var controller = new WasteTargetController(repo);
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
            new object[] { 100,new WasteTargetDataModel() { ID = 100, Dimension = "353x3512", Target = 0 } , "A 100 azonosítóval nem létezik Selejt Target Dimenzió a rendszerben" },
        };
    }
}
