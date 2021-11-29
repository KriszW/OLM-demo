using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Repositories.MoneyExchangeRate
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
            var newISOCode = "XXX";
            var model = new CurrencyModel()
            {
                ID = id,
                ISOCode = newISOCode
            };


            //Act
            var repo = new MoneyExchangeRateRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newISOCode, actual.ISOCode);
        }

        [Fact]
        public async void Modify_houldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var id = 100;
            var model = new CurrencyModel()
            {
                ID = id,
            };

            //Act
            var repo = new MoneyExchangeRateRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

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

            var id = 1;
            var model = new CurrencyModel()
            {
                ID = id,
                ISOCode = "EUR"
            };

            //Act
            var repo = new MoneyExchangeRateRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(CurrencyModel.ISOCode), actualException.ColumnName);
            Assert.Equal(model.ISOCode, actualException.ColumnValue);
        }
    }
}
