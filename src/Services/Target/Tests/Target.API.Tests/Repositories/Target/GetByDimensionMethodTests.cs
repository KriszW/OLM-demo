using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Services.Repositories.Implementations;
using OLM.Services.Target.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.Target.API.Tests.Repositories.Target
{
    public class GetByDimensionMethodTests
    {
        [Theory]
        [InlineData("25x75", false)]
        [InlineData("123x321", true)]
        [InlineData("", true)]
        [InlineData(default(string), true)]
        public async Task FetchByDimension(string dimension, bool isNull)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{dimension}-{isNull}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);

            //Act
            var repo = new TargetRepository(dbContext);
            var actual = await repo.GetByDimension(dimension);

            //Assert
            Assert.Equal(isNull, actual == default);
        }
    }
}
