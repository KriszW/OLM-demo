using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.TCO.API.Controllers;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Controllers.TCOContansValue
{
    public class DeleteActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var repo = new TCOSettingsRepository(dbContext);

            var id = 1;

            //Act
            var controller = new TCOConstansValueController(repo);
            var actionResult = await controller.Delete(id);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var repo = new TCOSettingsRepository(dbContext);

            var id = 100;
            var expectedMSG = $"A {id} azonosítóval nem létezik TCO konstansadatok a rendszerben";

            //Act
            var controller = new TCOConstansValueController(repo);
            var actionResult = await controller.Delete(id);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
