using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Repositories.MoneyExchangeRate
{
    public class AllMethodTests
    {
        [Fact]
        public async void All_ShouldReturnAll()
        {
            //Arrange 
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var expectedCount = 3;

            //Act
            var repo = new MoneyExchangeRateRepository(dbContext);
            var actual = await repo.All();

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actual.Count);
        }

        [Fact]
        public async void All_ShouldReturnNothingForNotUploadedDbContext()
        {
            //Arrange 
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            //Act
            var repo = new MoneyExchangeRateRepository(dbContext);
            var actual = await repo.All();

            //Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }
    }
}
