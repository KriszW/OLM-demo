using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Repositories.Pauses
{
    public class GetByIDMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(10, false)]
        [InlineData(0, true)]
        [InlineData(-1, true)]
        public async void GetByID_ShouldReturnSuccess(int id, bool isNull)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{isNull}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);

            //Act
            var repo = new PausesRepository(dbContext);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.Equal(isNull, actual == default);
        }
    }
}
