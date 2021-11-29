using Microsoft.AspNetCore.Mvc;
using OLM.Services.MoneyExchangeRate.API.Controllers;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.ExchangeRate;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Controllers.MoneyExchangeRate
{
    public class AddActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new MoneyExchangeRateRepository(dbContext);

            var model = new CurrencyModel
            {
                ID = null,
                ISOCode = "GPB"
            };

            //Act
            var controller = new MoneyExchangeRateController(repo);
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
        public async void ShouldReturnConflict(CurrencyModel model, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new MoneyExchangeRateRepository(dbContext);

            //Act
            var controller = new MoneyExchangeRateController(repo);
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
            new object[] { new CurrencyModel() { ISOCode = "EUR" } , "A EUR valutával már van feltöltve adat az adatbázisba" },
            new object[] { new CurrencyModel() { ISOCode = "GPB", ID = 1 }, "A 1 azonosítóval már létezik valuta a rendszerben" },
        };
    }
}
