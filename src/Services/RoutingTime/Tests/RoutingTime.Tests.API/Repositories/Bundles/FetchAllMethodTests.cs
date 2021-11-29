using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Repositories.Bundles
{
    public class FetchAllMethodTests
    {
        [Fact]
        public async Task FetchAll_ShouldReturnOkay()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);

            var machineName = "1";
            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now.AddDays(2);

            var expectedCount = 10;

            //Act
            var repo = new BundlesRepository(dbContext);
            var actual = await repo.FetchAll(machineName, start, end);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actual.Count);
        }

        [Fact]
        public async Task FetchAll_ShouldReturnEmpty()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);

            var machineName = "";
            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now.AddDays(2);

            //Act
            var repo = new BundlesRepository(dbContext);
            var actual = await repo.FetchAll(machineName, start, end);

            //Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }
    }
}
