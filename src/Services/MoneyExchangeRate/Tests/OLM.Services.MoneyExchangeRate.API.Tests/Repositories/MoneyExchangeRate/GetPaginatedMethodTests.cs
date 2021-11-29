using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Repositories.MoneyExchangeRate
{
    public class GetPaginatedMethodTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 2, 2)]
        [InlineData(0, 30, 3)]
        [InlineData(1, 30, 2)]
        [InlineData(60, 30, 0)]
        public async void GetPaginated_ShouldReturnSuccess(int skip, int take, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{skip}-{take}-{expectedCount}");
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);

            var expectedPageIndex = skip / take;

            //Act
            var repo = new MoneyExchangeRateRepository(dbContext);
            var actual = await repo.GetPaginated(skip, take);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPageIndex, actual.ActualPage);
            Assert.Equal(take, actual.PageSize);
            Assert.Equal(expectedCount, actual.Data.Count());
        }
    }
}
