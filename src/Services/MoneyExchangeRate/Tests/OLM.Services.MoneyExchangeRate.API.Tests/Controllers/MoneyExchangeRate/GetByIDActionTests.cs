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
    public class GetByIDActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new MoneyExchangeRateRepository(dbContext);

            var id = 1;

            //Act
            var controller = new MoneyExchangeRateController(repo);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<CurrencyModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<CurrencyModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
        }

        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new MoneyExchangeRateRepository(dbContext);

            var id = 100;
            var expectedMSG = $"A {id} azonosítóval nincs feltöltve valuta az adatbázisba";

            //Act
            var controller = new MoneyExchangeRateController(repo);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<CurrencyModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<CurrencyModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
