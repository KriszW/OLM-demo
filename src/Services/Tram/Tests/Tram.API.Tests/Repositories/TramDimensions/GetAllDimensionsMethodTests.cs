using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Repositories.TramDimensions
{
    public class GetAllDimensionsMethodTests
    {
        [Fact]
        public async void GetByID_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var expectedCount = 3;

            //Act
            var repo = new TramDimensionRepository(dbContext);
            var actual = await repo.GetAllDimension();

            //Assert
            Assert.Equal(expectedCount, actual.Count);
        }
    }
}
