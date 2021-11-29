using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations;
using OLM.Services.CategoryBulbs.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.CategoryBulbs.API.Tests.Repositories.BundleItemnumber
{
    public class HasCategoryMethodTests
    {
        [Theory]
        [InlineData("bundle1","1", true, "A bundle1 rakathoz van cikk a 1 kategoriában")]
        [InlineData("bundle1","2", false, "A bundle1 rakathoz nincsen cikk a 2 kategoriában")]
        [InlineData("bundle1","3", true, "A bundle1 rakathoz van cikk a 3 kategoriában")]
        [InlineData("bundle1","4", true, "A bundle1 rakathoz van cikk a 4 kategoriában")]
        [InlineData("bundle100","4", false, "A bundle100 rakat nem létezik az adatbázisban")]
        public async void GetByID_ShouldReturnSuccess(string bundleID, string category, bool expected, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{bundleID}-{category}-{expected}");
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            //Act
            var repo = new BundleItemnumberRepository(dbContext);
            var actual = await repo.HasCategory(bundleID, category);

            //Assert
            Assert.Equal(expected, actual.ValidationSucceded);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
