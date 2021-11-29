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
    public class GetByIDActionTests
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

            //Act
            var controller = new WasteTargetController(repo);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<WasteTargetDataModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<WasteTargetDataModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);
            var repo = new WasteTargetRepository(dbContext);

            var id = 100;
            var expectedMSG = $"A {id} azonosítóval nincs feltöltve Selejt Target Dimenzió az adatbázisba";

            //Act
            var controller = new WasteTargetController(repo);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<WasteTargetDataModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<WasteTargetDataModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
