using Microsoft.AspNetCore.Mvc;
using OLM.Services.CategoryBulbs.API.Controllers;
using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations;
using OLM.Services.CategoryBulbs.API.Tests.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.CategoryBulbs.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.CategoryBulbs.API.Tests.Controllers.Validator
{
    public class ValidateBundleActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);
            var repo = new BundleItemnumberRepository(dbContext);

            var bundleID = "bundle1";
            var expectedCount = 4;

            //Act
            var controller = new ValidatorController(repo);
            var actionResult = await controller.ValidateBundle(bundleID);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<IEnumerable<ValidationResult>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<ValidationResult>>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.Equal(expectedCount, actual.Model.Count());
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);
            var repo = new BundleItemnumberRepository(dbContext);

            var bundleID = "5";
            var expectedMSG = $"A {bundleID} rakat nem létezik az adatbázisban";

            //Act
            var controller = new ValidatorController(repo);
            var actionResult = await controller.ValidateBundle(bundleID);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<IEnumerable<ValidationResult>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<ValidationResult>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
