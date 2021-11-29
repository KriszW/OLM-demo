using Microsoft.AspNetCore.Mvc;
using OLM.Services.MoneyExchangeRate.API.Controllers;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Controllers.MoneyExchangeRate
{
    public class AllActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new MoneyExchangeRateRepository(dbContext);

            //Act
            var controller = new MoneyExchangeRateController(repo);
            var actionResult = await controller.All();

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<List<string>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<List<string>>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new MoneyExchangeRateRepository(dbContext);

            var expectedMSG = $"Nincs feltöltve valuta az adatbázisba";

            //Act
            var controller = new MoneyExchangeRateController(repo);
            var actionResult = await controller.All();

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<List<string>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<List<string>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
