using Microsoft.EntityFrameworkCore;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Repositories.ExchangeRate
{
    public class AddMethodTests
    {
        [Fact]
        public async void Add_ShouldSuccess()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var expectedID = (await dbContext.Currencies.MaxAsync(b => b.ID.GetValueOrDefault())) + 1;

            var sourceISOCode = "HUF";
            var model = new ExchangeRateModel()
            {
                ID = default,
                DestISOCode = "GPB"
            };

            //Act
            var repo = new ExchangeRateRepository(dbContext);
            await repo.Add(sourceISOCode,model);
            var actual = await repo.GetByID(expectedID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedID, actual.ID);
        }

        [Fact]
        public async void Add_ShouldThrowAlreadyExistsID()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var sourceISOCode = "HUF";
            var model = new ExchangeRateModel()
            {
                ID = 1,
                DestISOCode = "GPB"
            };

            //Act
            var repo = new ExchangeRateRepository(dbContext);
            var AddTask = repo.Add(sourceISOCode,model);

            //Assert
            var exception = await Assert.ThrowsAsync<PrimaryKeyAlreadyExistsException<int>>(() => AddTask);
            var actualException = Assert.IsAssignableFrom<PrimaryKeyAlreadyExistsException<int>>(exception);
            Assert.Equal(nameof(ExchangeRateModel.ID), actualException.ColumnName);
            Assert.Equal(model.ID, actualException.ColumnValue);
        }

        [Fact]
        public async void Add_ShouldThrowAlreadyExistsDestISOCode()
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var sourceISOCode = "HUF";
            var model = new ExchangeRateModel()
            {
                ID = default,
                DestISOCode = "EUR",
            };

            //Act
            var repo = new ExchangeRateRepository(dbContext);
            var AddTask = repo.Add(sourceISOCode,model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => AddTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(ExchangeRateModel.DestISOCode), actualException.ColumnName);
            Assert.Equal(model.DestISOCode, actualException.ColumnValue);
        }
    }
}
