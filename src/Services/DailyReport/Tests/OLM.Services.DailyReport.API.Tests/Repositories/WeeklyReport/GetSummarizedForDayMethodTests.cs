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
    public class GetSummarizedForDayMethodTests
    {
        [Fact]
        public async Task GetSummarizedForDay_ShouldReturnAllForTheSameDate()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);

            var date = DateTime.Now;
            var expectedAllLength = 36904;
            var expectedAllWaste = 6161;
            var expectedAllFS = 5076;

            //Act
            var service = new WeeklyReportRepository(dbContext);
            var result = await service.GetSummarizedForDay(date);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAllLength, result.AllLength);
            Assert.Equal(expectedAllWaste, result.AllWaste);
            Assert.Equal(expectedAllFS, result.AllFS);
        }
    }
}
