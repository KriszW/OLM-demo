using Microsoft.AspNetCore.Mvc;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Repositories.TCOData
{
    public class GetBundlesByIDsMethodTests
    {
        [Theory]
        [InlineData(new string[] { "bundle1" }, 1)]
        [InlineData(new string[] { "bundle1", "bundle2" }, 2)]
        [InlineData(new string[] { "bundle1", "bundle2", "bundle100" }, 2)]
        [InlineData(new string[] { "bundle1", "bundle100" }, 1)]
        [InlineData(new string[] { "bundle100", "bundle101" }, 0)]
        public async void FetchData(IEnumerable<string> bundleIDs, int expectedCount)
        {
            //Arrange
            var stringBuilder = new StringBuilder();
            var bundleIDsForConString = bundleIDs.Select(b => b + "-");

            foreach (var item in bundleIDsForConString)
            {
                stringBuilder.Append(item);
            }

            var conString = $"{stringBuilder}{expectedCount}";

            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + conString);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            //Act
            var repo = new TCODataRepository(dbContext);
            var actual = await repo.GetByBundleIDs(bundleIDs);

            //Assert
            Assert.Equal(expectedCount, actual.Count());
        }
    }
}
