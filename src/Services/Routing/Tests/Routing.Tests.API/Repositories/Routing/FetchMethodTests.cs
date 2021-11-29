using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Repositories.Routing
{
    public class FetchMethodTests
    {
        [Theory]
        [InlineData(new string[] { "25 * 75" }, 1)]
        [InlineData(new string[] { "25 * 75", "19 * 100" }, 2)]
        [InlineData(new string[] { "12 * 32" }, 0)]
        [InlineData(new string[] { "12 * 32", "32 * 12" }, 0)]
        [InlineData(new string[] { }, 0)]
        public async Task Fetch(IEnumerable<string> dims, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{string.Join("_", dims)}-{expectedCount}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);

            //Act
            var repo = new RoutingRepository(dbContext);
            var actual = await repo.Fetch(dims);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actual.Count());
        }
    }
}
