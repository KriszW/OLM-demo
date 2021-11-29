using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Repositories.ProductionTime
{
    public class FetchBetweenMethodTests
    {
        [Theory]
        [MemberData(nameof(CreateFetchData))]
        public async Task FetchBetween_ShouldReturnOkay(DateTime start, DateTime end, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{start}-{end}-{expectedCount}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);

            var machineName = "1";

            //Act
            var repo = new ProductionTimeRepository(dbContext);
            var actual = await repo.FetchBetween(machineName, start, end);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actual.Count);
        }


        public static object[][] CreateFetchData() => new object[][]
        {
            new object[] { DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(2), 2 },
            new object[] { DateTime.Now.Date, DateTime.Now.Date.AddHours(10), 0 },
            new object[] { DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddHours(12), 1 },
            new object[] { DateTime.Now.Date.AddDays(10), DateTime.Now.Date.AddDays(12), 0 },
        };
    }
}
