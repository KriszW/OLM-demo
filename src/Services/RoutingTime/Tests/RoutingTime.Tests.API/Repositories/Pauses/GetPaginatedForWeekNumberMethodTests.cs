using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Repositories.Pauses
{
    public class GetPaginatedForWeekNumberMethodTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 2, 1)]
        [InlineData(0, 30, 1)]
        [InlineData(1, 30, 1)]
        [InlineData(60, 30, 0)]
        public async void GetPaginatedForWeekNumber_ShouldReturnSuccess(int skip, int take, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{skip}-{take}-{expectedCount}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);

            var expectedPageIndex = skip / take;

            if (DateTime.Now.AddDays(1).DayOfWeek == DayOfWeek.Monday) expectedCount++;

            //Act
            var repo = new PausesRepository(dbContext);
            var actual = await repo.GetPaginatedForWeekNumber(skip, take);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPageIndex, actual.ActualPage);
            Assert.Equal(take, actual.PageSize);
            Assert.Equal(expectedCount, actual.Data.Count());
        }
    }
}
