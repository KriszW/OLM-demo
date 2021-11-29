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
    public class AddActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);
            var repo = new WasteTargetRepository(dbContext);

            var model = new WasteTargetDataModel()
            {
                Dimension = "24141x64231",
                Target = 0.213
            };

            //Act
            var controller = new WasteTargetController(repo);
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
        public async void ShouldReturnConflict(WasteTargetDataModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);
            var repo = new WasteTargetRepository(dbContext);

            //Act
            var controller = new WasteTargetController(repo);
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
            new object[] { new WasteTargetDataModel() { Dimension = "25x75", Target = 0.12 } , "A '25x75' Target Dimenzióval már van feltöltve Selejt Target Dimenzió az adatbázisba" },
            new object[] { new WasteTargetDataModel() { ID = 1, Dimension = "353x3512", Target = 0.12 } , "A 1 azonosítóval már létezik Selejt Target Dimenzió a rendszerben" },
        };
    }
}
