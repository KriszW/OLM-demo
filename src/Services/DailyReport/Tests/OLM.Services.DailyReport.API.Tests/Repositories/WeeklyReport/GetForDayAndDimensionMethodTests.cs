using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.DailyReport.API.Tests.Repositories.WeeklyReport
{
    public class GetForDayAndDimensionMethodTests
    {
        [Fact]
        public async Task GetForDay_ShouldReturnAllForTheSameDate()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);

            var date = DateTime.Now;
            var dimension = "25x75";
            var expectedCount = 3;

            //Act
            var service = new WeeklyReportRepository(dbContext);
            var result = await service.GetForDayAndDimension(date, dimension);

            //Assert
            Assert.Equal(expectedCount, result.Count());
        }
    }
}
