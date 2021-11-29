using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.Tram.API.Controllers;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Controllers.TramData
{
    public class ModifyActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramDataRepository(dbContext);

            var id = 1;
            var model = new TramDataModel()
            {
                ID = id,
                Dimension = new TramDimensionModel { Dimension = "25x75" }
            };

            //Act
            var controller = new TramDataController(repo);
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
        public async void ShouldReturnBadRequest(int id, TramDataModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramDataRepository(dbContext);


            //Act
            var controller = new TramDataController(repo);
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
            new object[] { 1,new TramDataModel() { ID = 0,  } , "A model IDja és a parmatér ID nem egyezik" },
        };

        [Theory]
        [MemberData(nameof(CreateConflictParameterData))]
        public async void ShouldReturnConflict(int id, TramDataModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramDataRepository(dbContext);

            //Act
            var controller = new TramDataController(repo);
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
           new object[] { 1,new TramDataModel() { ID = 1, Dimension = new TramDimensionModel { Dimension = "21x12" }  } , "A '21x12' dimenzióval nincs feltöltve csille dimenzió az adatbázisba" },
        };

        [Theory]
        [MemberData(nameof(CreateNotFoundParameterData))]
        public async void ShouldReturnNotFound(int id, TramDataModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramDataRepository(dbContext);

            //Act
            var controller = new TramDataController(repo);
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
            new object[] { 100,new TramDataModel() { ID = 100,  } , "A 100 azonosítóval nem létezik csille adat a rendszerben" },
        };
    }
}
