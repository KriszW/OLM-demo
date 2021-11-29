using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Repositories.TCOSettings
{
    public class GetByDimensionMethodTests
    {
        [Theory]
        [InlineData("5x25", false)]
        [InlineData("321x34", true)]
        [InlineData("", true)]
        public async void GetByDimension_ShouldReturnSuccess(string dimension, bool isNull)
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{dimension}-{isNull}");
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            //Act
            var repo = new TCOSettingsRepository(dbContext);
            var actual = await repo.GetByDimension(dimension);

            //Assert
            Assert.Equal(isNull, actual == default);
        }
    }
}
