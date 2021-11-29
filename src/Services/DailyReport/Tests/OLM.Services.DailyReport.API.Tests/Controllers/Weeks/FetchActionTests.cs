using Microsoft.AspNetCore.Mvc;
using Moq;
using OLM.Services.DailyReport.API.Controllers;
using OLM.Services.DailyReport.API.Data;
using OLM.Services.DailyReport.API.Services.Repositories.Implementations;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Weekly;
using OLM.Services.DailyReport.API.Services.Services.Implementations;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OLM.Services.DailyReport.API.Tests.FakeImplementations;
using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Implementations.Weeks;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;

namespace OLM.Services.DailyReport.API.Tests.Controllers.Weeks
{
    public class FetchActionTests
    {
        [Fact]
        public async void ShouldReturnSuccessForToday()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);
            var repo = new WeeksReportRepository(dbContext);

            var mockedTargetRepo = new Mock<ITargetRepository>();
            mockedTargetRepo.Setup(m => m.GetForDimension(new string[] { "25x75", "19x125", "38x75" }))
                .ReturnsAsync(new List<TargetResponseViewModel> {
                    new TargetResponseViewModel { Dimension = "25x75", Target = 0.16, Intersection = 0.0011875 },
                    new TargetResponseViewModel { Dimension = "19x125", Target = 0.1609, Intersection = 0.0018750 },
                    new TargetResponseViewModel { Dimension = "38x75", Target = 0.14, Intersection = 0.0008750 },
            });

            var aggregatorService = new DailyReportDataAggregationProviderService();
            var weeklyAggregatorService = new WeeksReportDataAggregatorService(mockedTargetRepo.Object, repo, aggregatorService);
            var service = new WeeksReportFetchService(weeklyAggregatorService);

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

            //Act
            var controller = new WeeksReportController(service);
            var actionResult = await controller.FetchWeekly(tramModels, new WeeksRequestViewModel(date.AddDays(-10),date));

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<WeeksReportResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<WeeksReportResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new DailyReportDbContext(dbOptions);
            var repo = new WeeksReportRepository(dbContext);
            var mockedTargetRepo = new Mock<ITargetRepository>();
            mockedTargetRepo.Setup(m => m.GetForDimension("19x125")).ReturnsAsync(new TargetResponseViewModel { Dimension = "19x125", Target = 0.010 });
            mockedTargetRepo.Setup(m => m.GetForDimension("25x75")).ReturnsAsync(new TargetResponseViewModel { Dimension = "25x75", Target = 0.011 });
            mockedTargetRepo.Setup(m => m.GetForDimension("38x75")).ReturnsAsync(new TargetResponseViewModel { Dimension = "25x75", Target = 0.011 });
            var aggregatorService = new DailyReportDataAggregationProviderService();
            var weeklyAggregatorService = new WeeksReportDataAggregatorService(mockedTargetRepo.Object, repo, aggregatorService);
            var service = new WeeksReportFetchService(weeklyAggregatorService);

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

            var date = DateTime.Now.AddDays(10);
            var model = new WeeksRequestViewModel(date, date.AddDays(10));

            var expectedMSG = $"A {model.Start}-től a {model.End}-ig nem található adat az adatbázisban";

            //Act
            var controller = new WeeksReportController(service);
            var actionResult = await controller.FetchWeekly(tramModels, model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<WeeksReportResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<WeeksReportResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
