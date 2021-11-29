using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Repositories.Pauses
{
    public class GetPaginatedForDataMethodTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 2, 2)]
        [InlineData(0, 30, 10)]
        [InlineData(1, 30, 9)]
        [InlineData(60, 30, 0)]
        public async void GetPaginatedForData_ShouldReturnSuccess(int skip, int take, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{skip}-{take}-{expectedCount}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);

            var expectedPageIndex = skip / take;

            var weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            if (DateTime.Now.AddDays(1).DayOfWeek == DayOfWeek.Monday) expectedCount -= 5;

            //Act
            var repo = new PausesRepository(dbContext);
            var actual = await repo.GetPaginatedForData(DateTime.Now.Year, weekNumber, skip, take);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPageIndex, actual.ActualPage);
            Assert.Equal(take, actual.PageSize);
            Assert.Equal(expectedCount, actual.Data.Count());
        }
    }
}
