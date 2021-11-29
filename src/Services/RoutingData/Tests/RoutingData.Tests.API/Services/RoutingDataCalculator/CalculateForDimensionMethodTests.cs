using Moq;
using OLM.Services.RoutingData.API.Services.Repositories.Abstractions;
using OLM.Services.RoutingData.API.Services.Services.Implementations;
using OLM.Services.RoutingData.Tests.API.FakeImplementations;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.RoutingData.Tests.API.Services.RoutingDataCalculator
{
    public class CalculateForDimensionMethodTests
    {
        [Theory]
        [InlineData("25 * 75", 36233.4)]
        [InlineData("19 * 125", 13625.400000000001)]
        public async Task Calculate_ShouldBeOk(string expectedDimension, double expectedAllLength)
        {
            //Act
            var model = new FetchRoutingDataRequestViewModel
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                MachineName = "1"
            };

            var mockedRoutingDataRepo = new Mock<IRoutingDataRepository>();
            mockedRoutingDataRepo.Setup(m => m.FetchData(model)).ReturnsAsync(FakeDbContextFactory.Models);

            //Arrange
            var service = new RoutingDataCalculatorService(mockedRoutingDataRepo.Object);
            var actualData = await service.FetchDataForDimension(model);

            var actual = actualData.FirstOrDefault(m => m.Dimension == expectedDimension);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedDimension, actual.Dimension);
            Assert.Equal(expectedAllLength, actual.AllLength);
        }
    }
}
