using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;
using OLM.Services.DailyReport.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.DailyReport.API.Tests.Repositories.WeeksReport
{
    public class GetDataForMethodTests
    {
        [Fact]
        public async Task GetForDay_ShouldReturnAllForTheSameDate()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);

            var model = new YearWeekStartEndViewModel(DateTime.Now.AddDays(-7), DateTime.Now,23, DateTime.Now.Year);
            var expectedCount = 19;

            //Act
            var service = new WeeksReportRepository(dbContext);
            var result = await service.GetDataFor(model);

            //Assert
            Assert.Equal(expectedCount, result.Count());
        }
    }
}
