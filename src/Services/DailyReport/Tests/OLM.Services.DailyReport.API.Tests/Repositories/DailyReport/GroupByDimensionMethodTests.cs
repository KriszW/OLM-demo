using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.DailyReport.API.Tests.Repositories.DailyReport
{
    public class GroupByDimensionMethodTests
    {
        [Fact]
        public async Task Fetch_ShouldReturnData()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);

            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now.AddDays(1);

            var groupCount = 4;

            //Act
            var repo = new DailyReportRepository(dbContext);
            var actual = await repo.GroupByDimension(start, end);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(groupCount, actual.Count());
        }

        [Fact]
        public async Task Fetch_ShouldReturnDefaultForOneYearBefore()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);

            var start = DateTime.Now.AddDays(10);
            var end = DateTime.Now.AddDays(11);

            //Act
            var repo = new DailyReportRepository(dbContext);
            var actual = await repo.GroupByDimension(start, end);

            //Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }
    }
}
