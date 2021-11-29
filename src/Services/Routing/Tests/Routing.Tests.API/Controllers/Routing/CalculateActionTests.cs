using Microsoft.AspNetCore.Mvc;
using Moq;
using OLM.Services.Routing.API.Controllers;
using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using OLM.Services.Routing.API.Services.Repositories.Abstractions;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.API.Services.Services.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Routing.SharedAPIModels.Request;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Response;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Controllers.Routing
{
    public class CalculateActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);

            var routingRepo = new RoutingRepository(dbContext);

            var model = new RoutingRequestViewModel
            {
                Start = DateTime.Now.Date,
                End = DateTime.Now.Date.AddDays(2),
                MachineName = "1"
            };

            var mockedRoutingTimeRepo = new Mock<IRoutingTimeRepository>();
            mockedRoutingTimeRepo.Setup(m => m.Fetch(model.Start, model.End, model.MachineName)).ReturnsAsync(new RoutingTimesResponseViewModel
            {
                Data = new List<RoutingTimesDataResponseViewModel>
                {
                    new RoutingTimesDataResponseViewModel
                    {
                        Dimension = "25 * 75",
                        AllTime = 1920,
                        PauseMinutes = 110,
                        ProductionMinutes = 1478
                    },
                    new RoutingTimesDataResponseViewModel
                    {
                        Dimension = "19 * 75",
                        AllTime = 1920,
                        PauseMinutes = 25,
                        ProductionMinutes = 307
                    },
                }
            });
            var mockedRoutingDataRepo = new Mock<IRoutingDataRepository>(); ;
            mockedRoutingDataRepo.Setup(m => m.Fetch(model.Start, model.End, model.MachineName)).ReturnsAsync(new RoutingDataResponseViewModel
            {
                Data = new List<RoutingDataDimensionResponseViewModel>
                {
                    new RoutingDataDimensionResponseViewModel
                    {
                        AllLength = 7700,
                        Dimension = "19 * 75"
                    },
                    new RoutingDataDimensionResponseViewModel
                    {
                        AllLength = 42228,
                        Dimension = "25 * 75"
                    },
                }
            });

            var service = new RoutingService(routingRepo, mockedRoutingTimeRepo.Object, mockedRoutingDataRepo.Object);

            //Act
            var controller = new RoutingController(service);
            var actionResult = await controller.Calculate(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<RoutingResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<RoutingResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);
            var repo = new RoutingManagerRepository(dbContext);

            var routingRepo = new RoutingRepository(dbContext);

            var model = new RoutingRequestViewModel
            {
                Start = DateTime.Now.Date,
                End = DateTime.Now.Date.AddDays(2),
                MachineName = "1"
            };

            var mockedRoutingTimeRepo = new Mock<IRoutingTimeRepository>();
            mockedRoutingTimeRepo.Setup(m => m.Fetch(model.Start, model.End, model.MachineName)).ReturnsAsync(new RoutingTimesResponseViewModel
            {
                Data = new List<RoutingTimesDataResponseViewModel>
                {
                    new RoutingTimesDataResponseViewModel
                    {
                        Dimension = "25 * 75",
                        AllTime = 1920,
                        PauseMinutes = 110,
                        ProductionMinutes = 1478
                    },
                    new RoutingTimesDataResponseViewModel
                    {
                        Dimension = "19 * 75",
                        AllTime = 1920,
                        PauseMinutes = 25,
                        ProductionMinutes = 307
                    },
                }
            });
            var mockedRoutingDataRepo = new Mock<IRoutingDataRepository>(); ;
            mockedRoutingDataRepo.Setup(m => m.Fetch(model.Start, model.End, model.MachineName)).ReturnsAsync(default(RoutingDataResponseViewModel));

            var service = new RoutingService(routingRepo, mockedRoutingTimeRepo.Object, mockedRoutingDataRepo.Object);

            var expectedMSG = $"{model.Start}-tól {model.End}-ig nem számolható ki a Routing a '{model.MachineName}' szabászsorra";

            //Act
            var controller = new RoutingController(service);
            var actionResult = await controller.Calculate(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<RoutingResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<RoutingResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
