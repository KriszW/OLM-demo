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

namespace OLM.Services.Tram.API.Tests.Controllers.TramDimension
{
    public class AddActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramDimensionRepository(dbContext);

            var model = new TramDimensionModel()
            {
                Dimension = "43x23"
            };

            //Act
            var controller = new TramDimensionController(repo);
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
        public async void ShouldReturnConflict(TramDimensionModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{expectedMSG}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramDimensionRepository(dbContext);

            //Act
            var controller = new TramDimensionController(repo);
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
            new object[] { new TramDimensionModel() { Dimension = "25x75" } , "A '25x75' dimenzió már fel van töltve az adatbázisba" },
            new object[] { new TramDimensionModel() { ID = 1, } , "A 1 azonosítóval már van csille dimenzió a rendszerben" },
        };
    }
}
