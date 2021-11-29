using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Services.TCO.API.Tests.TestExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Repositories.TCOSettings
{
    public class GetForDimensionsMethodTests
    {
        [Theory]
        [InlineData(new string[] { "5x25" }, 1)]
        [InlineData(new string[] { "5x25", "13x25" }, 2)]
        [InlineData(new string[] { "5x25", "13x25", "5342x214" }, 2)]
        [InlineData(new string[] { "5x25", "5342x214" }, 1)]
        [InlineData(new string[] { "5342x214", "631x7432" }, 0)]
        public async void FetchData(IEnumerable<string> dimensions, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{dimensions.ConvertDataIntoOneString()}{expectedCount}");
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            //Act
            var repo = new TCOSettingsRepository(dbContext);
            var actual = await repo.GetForDimensions(dimensions);

            //Assert
            Assert.Equal(expectedCount, actual.Count());
        }
    }
}
