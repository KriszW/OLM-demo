using Moq;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Weekly;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.DailyReport.API.Tests.Services.WeeklyDataAggregator
{
    public class AggregateForDayMethodTests
    {
        [Fact]
        public async Task Aggregate_ShouldReturnOkay()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);
            var repo = new WeeklyReportRepository(dbContext);

            var date = DateTime.Now;

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
            };

            var mockedTargetRepo = new Mock<ITargetRepository>();
            mockedTargetRepo.Setup(m => m.GetForDimension(new string[] { "25x75", "19x125" }))
                .ReturnsAsync(new List<TargetResponseViewModel> { 
                    new TargetResponseViewModel { Dimension = "25x75", Target = 0.16, Intersection = 0.0011875 },
                    new TargetResponseViewModel { Dimension = "19x125", Target = 0.1609, Intersection = 0.0018750 },
                });

            var aggregatorService = new DailyReportDataAggregationProviderService();

            var expectedSawPercent = 0.23242283100832106;
            var expectedFSPercent = 0.10148219530102789;
            var expectedLamPercent = 5.73604992657856E-05;
            var expectedTramPercent = 0.0;
            var expectedExcludedPlankPercent = 0.0;
            var expectedTotalWastePercent = 0.33396238680861473;
            var expectedTargetPercent = 0.16035620870044051;

            //Act
            var service = new WeeklyReportDataAggregatorService(repo, mockedTargetRepo.Object, aggregatorService);
            var actual = await service.AggregateForDay(date, tramModels);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(date, actual.Date, new TimeSpan(0,1,0));
            Assert.Equal(expectedSawPercent, actual.TotalSawPercent);
            Assert.Equal(expectedFSPercent, actual.TotalFSPercent);
            Assert.Equal(expectedLamPercent, actual.TotalLamellaPercent);
            Assert.Equal(expectedTramPercent, actual.TotalTramPercent);
            Assert.Equal(expectedExcludedPlankPercent, actual.TotalExcludedPlankPercent);
            Assert.Equal(expectedTotalWastePercent, actual.TotalWastePercent);
            Assert.Equal(expectedTargetPercent, actual.TotalTargetPercent);
        }
    }
}
