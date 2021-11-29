using Microsoft.AspNetCore.Mvc;
using Moq;
using OLM.Services.RoutingData.API.Controllers;
using OLM.Services.RoutingData.API.Services.Services.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingData.Tests.API.Controllers.RoutingData
{
    public class CalculateRoutingDataMethodTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var now = DateTime.Now;

            var model = new FetchRoutingDataRequestViewModel
            {
                MachineName = "1",
                Start = now,
                End = now.AddDays(1),
            };

            var mockedService = new Mock<IRoutingDataCalculatorService>();
            mockedService.Setup(m => m.FetchDataForDimension(model)).ReturnsAsync(new List<RoutingDataDimensionResponseViewModel>() { new RoutingDataDimensionResponseViewModel() });

            //Act
            var controller = new RoutingDataController(mockedService.Object);
            var actionResult = await controller.CalculateRoutingData(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<RoutingDataResponseViewModel>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<RoutingDataResponseViewModel>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var now = DateTime.Now;

            var model = new FetchRoutingDataRequestViewModel
            {
                MachineName = "1",
                Start = now.AddDays(10),
                End = now.AddDays(11),
            };

            var mockedService = new Mock<IRoutingDataCalculatorService>();
            mockedService.Setup(m => m.FetchDataForDimension(model)).ReturnsAsync(new List<RoutingDataDimensionResponseViewModel>() { });

            var expectedMSG = $"{model.Start} és {model.End} között nem található rakat adat a rendszerben a {model.MachineName} géphez";

            //Act
            var controller = new RoutingDataController(mockedService.Object);
            var actionResult = await controller.CalculateRoutingData(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<RoutingDataResponseViewModel>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<RoutingDataResponseViewModel>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
