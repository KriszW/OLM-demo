using Microsoft.AspNetCore.Mvc;
using OLM.Services.CategoryBulbs.API.Controllers;
using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations;
using OLM.Services.CategoryBulbs.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.CategoryBulbs.API.Tests.Controllers.CategoryItemNumber
{
    public class DeleteActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);
            var repo = new ItemnumberCategoryRepository(dbContext);

            var id = 1;

            //Act
            var controller = new CategoryItemNumberController(repo);
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
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);
            var repo = new ItemnumberCategoryRepository(dbContext);

            var id = 100;
            var expectedMSG = $"A {id} azonosítóval nem létezik cikk kategória a rendszerben";

            //Act
            var controller = new CategoryItemNumberController(repo);
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
