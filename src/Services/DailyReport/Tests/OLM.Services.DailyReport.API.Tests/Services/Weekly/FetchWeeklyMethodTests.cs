using Moq;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Daily;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Weekly;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.DailyReport.API.Tests.Services.Weekly
{
    public class FetchWeeklyMethodTests
    {
        [Fact]
        public async Task FetchWeekly_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);
            var repo = new WeeklyReportRepository(dbContext);

            var mockedTargetRepo = new Mock<ITargetRepository>();
            mockedTargetRepo.Setup(m => m.GetForDimension(new string[] { "25x75", "19x125", "38x75" }))
                .ReturnsAsync(new List<TargetResponseViewModel> {
                    new TargetResponseViewModel { Dimension = "25x75", Target = 0.16, Intersection = 0.0011875 },
                    new TargetResponseViewModel { Dimension = "19x125", Target = 0.1609, Intersection = 0.0018750 },
                    new TargetResponseViewModel { Dimension = "38x75", Target = 0.14, Intersection = 0.0008750 },
            });

            var aggregatorService = new DailyReportDataAggregationProviderService();
            var fetchService = new WeeklyReportDataAggregatorService(repo, mockedTargetRepo.Object, aggregatorService);

            var tramModels = new List<DailyReportRequestTramViewModel>
            {
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "25x75",
                    NumberOfLammela = 0,
                    NumberOfTram = 0,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "19x125",
                    NumberOfLammela = 1,
                    NumberOfTram = 0,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "38x75",
                    NumberOfLammela = 0,
                    NumberOfTram = 1,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now.AddDays(-1),
                    Dimension = "25x75",
                    NumberOfLammela = 1,
                    NumberOfTram = 1,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now.AddDays(-1),
                    Dimension = "19x125",
                    NumberOfLammela = 2,
                    NumberOfTram = 0,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now.AddDays(-1),
                    Dimension = "38x75",
                    NumberOfLammela = 2,
                    NumberOfTram = 2,
                },
            };

            var date = DateTime.Now;
            var expectedCount = 2;

            if (date.DayOfWeek == DayOfWeek.Monday) expectedCount--;

            //Act
            var service = new WeeklyReportFetchService(fetchService);
            var result = await service.FetchWeekly(date, tramModels);

            //Assert
            Assert.Equal(expectedCount, result.Models.Count());
        }
    }
}
