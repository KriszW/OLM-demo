using OLM.Services.RoutingData.API.Data;
using OLM.Services.RoutingData.API.Services.Repositories.Implementations;
using OLM.Services.RoutingData.Tests.API.FakeImplementations;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.RoutingData.Tests.API.Repositories.RoutingData
{
    public class FetchDataMethodTests
    {
        [Fact]
        public async Task FetchData_ShouldReturnOkay()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDataDbContext(dbOptions);

            var model = new FetchRoutingDataRequestViewModel
            {
                MachineName = "1",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(2)
            };

            var expectedCount = 10;

            //Act
            var repo = new RoutingDataRepository(dbContext);
            var actual = await repo.FetchData(model);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actual.Count);
        }

        [Fact]
        public async Task FetchData_ShouldReturnEmpty()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDataDbContext(dbOptions);

            var model = new FetchRoutingDataRequestViewModel
            {
                MachineName = "1",
                Start = DateTime.Now.AddDays(10),
                End = DateTime.Now.AddDays(15)
            };

            //Act
            var repo = new RoutingDataRepository(dbContext);
            var actual = await repo.FetchData(model);

            //Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }
    }
}
