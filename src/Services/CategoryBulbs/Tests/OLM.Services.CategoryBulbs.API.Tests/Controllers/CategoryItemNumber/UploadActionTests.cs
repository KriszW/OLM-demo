using Microsoft.AspNetCore.Mvc;
using OLM.Services.CategoryBulbs.API.Controllers;
using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Models;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations;
using OLM.Services.CategoryBulbs.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.CategoryBulbs.API.Tests.Controllers.CategoryItemNumber
{
    public class UploadActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);
            var repo = new ItemnumberCategoryRepository(dbContext);

            var model = new ItemnumberCategoryModel()
            {
                Itemnumber = "11656061YJ",
                CategoryType = "4"
            };

            //Act
            var controller = new CategoryItemNumberController(repo);
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
        public async void ShouldReturnConflict(ItemnumberCategoryModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);
            var repo = new ItemnumberCategoryRepository(dbContext);

            //Act
            var controller = new CategoryItemNumberController(repo);
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
            new object[] { new ItemnumberCategoryModel() { Itemnumber = "10113556MK" } , "A 10113556MK cikkszámmal már van feltöltve adat az adatbázisba" },
            new object[] { new ItemnumberCategoryModel() { ID = 1, Itemnumber = "10536376AS" } , "A 1 azonosítóval már létezik cikk kategória a rendszerben" },
        };
    }
}
