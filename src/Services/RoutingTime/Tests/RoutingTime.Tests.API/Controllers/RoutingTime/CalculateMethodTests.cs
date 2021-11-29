using Microsoft.AspNetCore.Mvc;
using Moq;
using OLM.Services.RoutingTime.API.Controllers;
using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.API.Services.Services.Abstractions;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Controllers.RoutingTime
{
    public class CalculateMethodTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var now = DateTime.Now;

            var model = new FetchRoutingRequestTimeViewModel
            {
                MachineName = "1",
                Start = now,
                End = now.AddDays(1),
            };

            var mockedService = new Mock<IRoutingTimeCalculaterService>();
            mockedService.Setup(m => m.Calculate(model.MachineName, model.Start, model.End)).ReturnsAsync(new List<RoutingTimesDataResponseViewModel>() { new RoutingTimesDataResponseViewModel() });

            //Act
            var controller = new RoutingTimeController(mockedService.Object);
            var actionResult = await controller.Calculate(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<RoutingTimesResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<RoutingTimesResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var now = DateTime.Now;

            var model = new FetchRoutingRequestTimeViewModel
            {
                MachineName = "1",
                Start = now.AddDays(10),
                End = now.AddDays(11),
            };

            var mockedService = new Mock<IRoutingTimeCalculaterService>();
            mockedService.Setup(m => m.Calculate(model.MachineName, model.Start, model.End)).ReturnsAsync(new List<RoutingTimesDataResponseViewModel>());

            var expectedMSG = $"{model.Start} és {model.End} között nem elérhető a routing idő a {model.MachineName} gépre";

            //Act
            var controller = new RoutingTimeController(mockedService.Object);
            var actionResult = await controller.Calculate(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<RoutingTimesResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<RoutingTimesResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
