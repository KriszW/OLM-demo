using Moq;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Weekly;
using OLM.Services.DailyReport.API.Services.Services.Implementations;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Weeks;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;

namespace OLM.Services.DailyReport.API.Tests.Services.WeeksDataAggregator
{
    public class AggregateMethodTests
    {
        [Fact]
        public async Task Aggregate_ShouldReturnOkay()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);
            var repo = new WeeksReportRepository(dbContext);

            var model = new YearWeekStartEndViewModel(DateTime.Now.AddDays(-2),DateTime.Now.AddDays(2),23,DateTime.Now.Year);

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

            var expectedSawPercent = 0.17165788740040791;
            var expectedFSPercent = 0.11874752395345767;
            var expectedLamPercent = 3.263964603052496E-05;
            var expectedTramPercent = 0.0019164562806913738;
            var expectedExcludedPlankPercent = 0.0;
            var expectedTotalWastePercent = 0.29235450728058748;
            var expectedTargetPercent = 0.16055102040816324;

            //Act
            var service = new WeeksReportDataAggregatorService(mockedTargetRepo.Object, repo, aggregatorService);
            var actual = await service.Aggregate(model, tramModels);

            //Assert
            Assert.NotNull(actual);
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
