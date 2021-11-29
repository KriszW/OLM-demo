using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Services.Repositories.Implementations;
using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Services.Repositories.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using OLM.Services.Target.API.Tests.FakeImplementations;

namespace OLM.Services.Target.API.Tests.Repositories.WasteTarget
{
    public class GetPaginatedMethodTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 2, 2)]
        [InlineData(0, 30, 4)]
        [InlineData(1, 30, 3)]
        [InlineData(60, 30, 0)]
        public async void GetPaginated_ShouldReturnSuccess(int skip, int take, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{skip}-{take}-{expectedCount}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);

            var expectedPageIndex = skip / take;

            //Act
            var repo = new WasteTargetRepository(dbContext);
            var actual = await repo.Paginate(skip, take);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPageIndex, actual.ActualPage);
            Assert.Equal(take, actual.PageSize);
            Assert.Equal(expectedCount, actual.Data.Count());
        }
    }
}
