using Moq;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Services.Services.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Repositories.Exchange
{
    public class GetRateByISOCodesMethodTests
    {
        [Fact]
        public async void Exchange_ShouldExchangeReturnForEURToHUF()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var sourceCurrency = "EUR";
            var destCurrency = "HUF";

            var expectedRate = 351.1M;

            //Act
            var service = new ExchangeRepository(dbContext);
            var actual = await service.GetRateByISOCodes(sourceCurrency, destCurrency);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedRate, actual.Rate);
        }

        [Fact]
        public async void Exchange_ShouldExchangeThrowNotFoundByValueExceptionForSource()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var sourceCurrency = "GPB";
            var destCurrency = "HUF";

            //Act
            var service = new ExchangeRepository(dbContext);
            var actualTask = service.GetRateByISOCodes(sourceCurrency, destCurrency);

            //Assert
            var actualException = await Assert.ThrowsAsync<NotFoundByValueException<string>>(() => actualTask);
            Assert.Equal("sourceISOCode", actualException.ColumnName);
            Assert.Equal(sourceCurrency, actualException.ColumnValue);
        }

        [Fact]
        public async void Exchange_ShouldExchangeThrowNotFoundByValueExceptionForDestination()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var sourceCurrency = "EUR";
            var destCurrency = "GPB";

            //Act
            var service = new ExchangeRepository(dbContext);
            var actualTask = service.GetRateByISOCodes(sourceCurrency, destCurrency);

            //Assert
            var actualException = await Assert.ThrowsAsync<NotFoundByValueException<string>>(() => actualTask);
            Assert.Equal("destISOCode", actualException.ColumnName);
            Assert.Equal(destCurrency, actualException.ColumnValue);
        }
    }
}
