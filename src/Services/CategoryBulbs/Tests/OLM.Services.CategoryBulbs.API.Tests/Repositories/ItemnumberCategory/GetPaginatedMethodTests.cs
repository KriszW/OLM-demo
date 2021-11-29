using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations;
using OLM.Services.CategoryBulbs.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.CategoryBulbs.API.Tests.Repositories.ItemnumberCategory
{
    public class GetPaginatedMethodTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 2, 2)]
        [InlineData(0, 30, 7)]
        [InlineData(1, 30, 6)]
        [InlineData(60, 30, 0)]
        public async void GetPaginated_ShouldReturnSuccess(int skip, int take, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{skip}-{take}-{expectedCount}");
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            var expectedPageIndex = skip / take;

            //Act
            var repo = new ItemnumberCategoryRepository(dbContext);
            var actual = await repo.GetPaginated(skip, take);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPageIndex, actual.ActualPage);
            Assert.Equal(take, actual.PageSize);
            Assert.Equal(expectedCount, actual.Data.Count());
        }
    }
}
