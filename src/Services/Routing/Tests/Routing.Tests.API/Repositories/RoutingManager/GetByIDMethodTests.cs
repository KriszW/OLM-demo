using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Repositories.RoutingManager
{
    public class GetByIDMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(10, true)]
        [InlineData(0, true)]
        [InlineData(-1, true)]
        public async void GetByID_ShouldReturnSuccess(int id, bool isNull)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{isNull}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);

            //Act
            var repo = new RoutingManagerRepository(dbContext);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.Equal(isNull, actual == default);
        }
    }
}
