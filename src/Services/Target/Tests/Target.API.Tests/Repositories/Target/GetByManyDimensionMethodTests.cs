using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Services.Repositories.Implementations;
using OLM.Services.Target.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.Target.API.Tests.Repositories.Target
{
    public class GetByManyDimensionMethodTests
    {
        [Theory]
        [InlineData(new string[] { "25x75", "19x125","38x75" }, 3)]
        [InlineData(new string[] { "25x75", "19x125","123x431" }, 2)]
        [InlineData(new string[] { "1x1", "123x12","12x321" }, 0)]
        public async Task FetchbyDimension(IEnumerable<string> dimension, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{dimension.FirstOrDefault()}-{expectedCount}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);

            //Act
            var repo = new TargetRepository(dbContext);
            var actual = await repo.GetByDimension(dimension);

            //Assert
            Assert.Equal(expectedCount, actual.Count);
        }
    }
}
