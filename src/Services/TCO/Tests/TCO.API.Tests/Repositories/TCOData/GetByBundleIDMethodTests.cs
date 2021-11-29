using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Repositories.TCOData
{
    public class GetByBundleIDMethodTests
    {
        [Theory]
        [InlineData("bundle1", false)]
        [InlineData("bundle100", true)]
        public async void FetchData(string bundleID, bool expectedToBeNull)
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{bundleID}-{expectedToBeNull}");
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            //Act
            var repo = new TCODataRepository(dbContext);
            var actual = await repo.GetByBundleID(bundleID);

            //Assert
            Assert.Equal(expectedToBeNull, actual == default);
        }
    }
}
