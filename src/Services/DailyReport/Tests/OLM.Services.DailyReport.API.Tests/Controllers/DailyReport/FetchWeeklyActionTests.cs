using Microsoft.AspNetCore.Mvc;
using Moq;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Daily;
using OLM.Services.DailyReport.API.Services.Services.Implementations;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OLM.Services.DailyReport.API.Controllers;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;

namespace OLM.Services.DailyReport.API.Tests.Controllers.DailyReport
{
    public class FetchWeeklyActionTests
    {
        [Fact]
        public async void ShouldReturnSuccessForToday()
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
            var service = new DailyReportDataFetchService(repo, mockedTargetRepo.Object, aggregatorService);

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

            var date = DateTime.Now;

            //Act
            var controller = new DailyReportController(service);
            var actionResult = await controller.FetchWeekly(tramData, date);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<DimensionReportSummarizedResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<DimensionReportSummarizedResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
            Assert.Equal(date, actual.Model.Date, new TimeSpan(0, 1, 0));
        }
        [Fact]
        public async void ShouldReturnNotFound()
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
            var service = new DailyReportDataFetchService(repo, mockedTargetRepo.Object, aggregatorService);

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

            var expectedMSG = $"A {date} időponthoz nem lehet lekérdezni a dimenzió selejt heti összesítését";

            //Act
            var controller = new DailyReportController(service);
            var actionResult = await controller.FetchWeekly(tramData, date);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<DimensionReportSummarizedResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<DimensionReportSummarizedResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
