using Moq;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions;
using OLM.Services.MoneyExchangeRate.API.Services.Services.Implementations;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Services.CurrencyExchange
{
    public class ExchangeMethodTests
    {
        [Fact]
        public async void Exchange_ShouldExchangeEURToHUF()
        {
            //Arrange
            var model = new ExchangeCurrencyViewModel
            {
                SourceCurrency = "EUR",
                DestinationCurrency = "HUF"
            };
            var expectedValue = 351.3M;

            var exchangeRepo = new Mock<IExchangeRepository>();
            exchangeRepo.Setup(m => m.GetRateByISOCodes(model.SourceCurrency, model.DestinationCurrency))
                .ReturnsAsync(new ExchangeRateModel() { Rate = expectedValue });

            //Act
            var service = new CurrencyExchangeService(exchangeRepo.Object, default);
            var actual = await service.Exchange(model);

            //Assert
            Assert.Equal(expectedValue, actual);
            exchangeRepo.Verify(m => m.GetRateByISOCodes(model.SourceCurrency, model.DestinationCurrency), Times.Once());
        }

        [Fact]
        public async void Exchange_ShouldReturnMinus1ForUnknownDestinationCurrency()
        {
            //Arrange
            var model = new ExchangeCurrencyViewModel
            {
                SourceCurrency = "HUF",
                DestinationCurrency = ""
            };
            var expectedValue = CurrencyExchangeService.ErrorOutputRate;

            var exchangeRepo = new Mock<IExchangeRepository>();
            exchangeRepo.Setup(m => m.GetRateByISOCodes(model.SourceCurrency, model.DestinationCurrency))
                .ReturnsAsync(default(ExchangeRateModel));

            //Act
            var service = new CurrencyExchangeService(exchangeRepo.Object, default);
            var actual = await service.Exchange(model);

            //Assert
            Assert.Equal(expectedValue, actual);
            exchangeRepo.Verify(m => m.GetRateByISOCodes(model.SourceCurrency, model.DestinationCurrency), Times.Once());
        }
    }
}
