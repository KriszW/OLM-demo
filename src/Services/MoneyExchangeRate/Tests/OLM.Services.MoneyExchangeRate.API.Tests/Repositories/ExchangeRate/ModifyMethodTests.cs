using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Services.MoneyExchangeRate.API.ViewModels;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Repositories.ExchangeRate
{
    public class ModifyMethodTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var id = 1;
            var model = new ModifyExchangeRateForCurrencyViewModel()
            {
                SourceISOCode = "HUF",
                Data = new ExchangeRateModel
                {
                    ID = id,
                    DestISOCode = "GPB",
                }
            };


            //Act
            var repo = new ExchangeRateRepository(dbContext);
            await repo.Modify(model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(model.Data.DestISOCode, actual.DestISOCode);
        }

        [Fact]
        public async void Modify_houldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var id = 100;
            var model = new ModifyExchangeRateForCurrencyViewModel()
            {
                SourceISOCode = "HUF",
                Data = new ExchangeRateModel
                {
                    ID = id,
                    DestISOCode = "GPB",
                }
            };

            //Act
            var repo = new ExchangeRateRepository(dbContext);
            var uploadTask = repo.Modify(model);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(CurrencyModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }

        [Fact]
        public async void Modify_ShouldThrowAlreadyISOCode()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var id = 6;
            var model = new ModifyExchangeRateForCurrencyViewModel()
            {
                SourceISOCode = "HUF",
                Data = new ExchangeRateModel
                {
                    ID = id,
                    DestISOCode = "EUR",
                }
            };

            //Act
            var repo = new ExchangeRateRepository(dbContext);
            var uploadTask = repo.Modify(model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(ExchangeRateModel.DestISOCode), actualException.ColumnName);
            Assert.Equal(model.Data.DestISOCode, actualException.ColumnValue);
        }
    }
}
