using Moq;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Daily;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.DailyReport.API.Tests.Services.DailyReportDataFetch
{
    public class FetchDailyMethodTests
    {
        [Fact]
        public async Task Fetch_ShouldReturnOkay()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);

            var repo = new DailyReportRepository(dbContext);

            var mockedTargetRepo = new Mock<ITargetRepository>();
            mockedTargetRepo.Setup(m => m.GetForDimension("19x125")).ReturnsAsync(new TargetResponseViewModel { Dimension = "19x125", Target = 0.010 });
            mockedTargetRepo.Setup(m => m.GetForDimension("25x75")).ReturnsAsync(new TargetResponseViewModel { Dimension = "25x75", Target = 0.011 });
            mockedTargetRepo.Setup(m => m.GetForDimension("38x75")).ReturnsAsync(new TargetResponseViewModel { Dimension = "38x75", Target = 0.011 });
            mockedTargetRepo.Setup(m => m.GetForDimension("13x25")).ReturnsAsync(new TargetResponseViewModel { Dimension = "13x25", Target = 0.011 });

            var aggregatorService = new DailyReportDataAggregationProviderService();

            var tramData = new List<DailyReportRequestTramViewModel>
            {
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "25x75",
                    NumberOfLammela = 2,
                    NumberOfTram = 0,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "19x125",
                    NumberOfLammela = 2,
                    NumberOfTram = 1,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "38x75",
                    NumberOfLammela = 2,
                    NumberOfTram = 1,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "13x25",
                    NumberOfLammela = 2,
                    NumberOfTram = 1,
                },
            };

            var date = DateTime.Now;
            var expectedCount = 4;

            //Act
            var service = new DailyReportDataFetchService(repo, mockedTargetRepo.Object, aggregatorService);
            var actual = await service.FetchDaily(date, tramData);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(date, actual.Date, new TimeSpan(0,1,0));
            Assert.Equal(expectedCount, actual.DimensionReportData.Count());
        }

        [Fact]
        public async Task Fetch_ShouldReturnDefaultForNotDayWhereNoDataExists()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);

            var repo = new DailyReportRepository(dbContext);
            var mockedTargetRepo = new Mock<ITargetRepository>();
            mockedTargetRepo.Setup(m => m.GetForDimension("19x125")).ReturnsAsync(new TargetResponseViewModel { Dimension = "19x125", Target = 0.010 });
            mockedTargetRepo.Setup(m => m.GetForDimension("25x75")).ReturnsAsync(new TargetResponseViewModel { Dimension = "25x75", Target = 0.011 });

            var aggregatorService = new DailyReportDataAggregationProviderService();

            var tramData = new List<DailyReportRequestTramViewModel>
            {
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "25x75",
                    NumberOfLammela = 2,
                    NumberOfTram = 0,
                },
                new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "19x125",
                    NumberOfLammela = 2,
                    NumberOfTram = 1,
                },
            };

            var date = DateTime.Now.AddDays(10);

            //Act
            var service = new DailyReportDataFetchService(repo, mockedTargetRepo.Object, aggregatorService);
            var actual = await service.FetchDaily(date, tramData);

            //Assert
            Assert.Null(actual);
        }
    }
}
