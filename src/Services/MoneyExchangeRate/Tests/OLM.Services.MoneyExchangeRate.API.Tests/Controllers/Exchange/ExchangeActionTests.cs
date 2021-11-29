using Microsoft.AspNetCore.Mvc;
using OLM.Services.MoneyExchangeRate.API.Controllers;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Services.Services.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Controllers.Exchange
{
    public class ExchangeActionTests
    {
        [Fact]
        public async Task Exchange_ShouldSuccess()
        {
            //Act
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new ExchangeRepository(dbContext);
            var service = new CurrencyExchangeService(repo, dbContext);

            var model = new ExchangeCurrencyViewModel
            {
                SourceCurrency = "HUF",
                DestinationCurrency = "EUR"
            };

            var expectedRate = 0.0029M;

            //Arrange
            var controller = new ExchangeController(service);
            var actionResult = await controller.Exchange(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<CurrencyExchangedDataViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<CurrencyExchangedDataViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
            Assert.Equal(expectedRate, actual.Model.Rate);
        }

        [Fact]
        public async Task Exchange_ShouldReturnFailed()
        {
            //Act
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new ExchangeRepository(dbContext);
            var service = new CurrencyExchangeService(repo, dbContext);

            var model = new ExchangeCurrencyViewModel
            {
                SourceCurrency = "HUF",
                DestinationCurrency = "XXX"
            };

            //Arrange
            var controller = new ExchangeController(service);
            var actionResult = await controller.Exchange(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<CurrencyExchangedDataViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<CurrencyExchangedDataViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Null(actual.Model);
            Assert.Equal(model.DestinationCurrency, actual.Message);
        }
    }
}
