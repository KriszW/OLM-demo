using Moq;
using OLM.Services.RoutingTime.API.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.API.Services.Services.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Services.RoutingTimeCalculator
{
    public class CalculateMethodTests
    {
        [Fact]
        public async Task Calculate_ShouldBeOk()
        {
            //Act
            var machineName = "1";

            var start = DateTime.Now;
            var end = DateTime.Now.Date.AddDays(2);

            var expectedDimension = "25 * 75";

            var expectedAllTime = 1920;
            var expectedAllProdMinutes = 1454;
            var expectedAllPauseMinutes = 100;

            var mockedProdTimeRepo = new Mock<IProductionTimeRepository>();
            mockedProdTimeRepo.Setup(m => m.FetchBetween(machineName, start, end)).ReturnsAsync(FakeDbContextFactory.CreateProductionTimes());
            var mockedPauseRepo = new Mock<IPausesRepository>();
            mockedPauseRepo.Setup(m => m.FetchBetween(machineName, start, end)).ReturnsAsync(FakeDbContextFactory.CreatePauses());
            var mockedBundleRepo = new Mock<IBundlesRepository>();
            mockedBundleRepo.Setup(m => m.FetchAll(machineName, start, end)).ReturnsAsync(FakeDbContextFactory.CreateData);

            //Arrange
            var service = new RoutingTimeCalculaterService(mockedProdTimeRepo.Object, mockedPauseRepo.Object, mockedBundleRepo.Object);
            var actualData = await service.Calculate(machineName, start, end);

            var actual = actualData.FirstOrDefault(m => m.Dimension == expectedDimension);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedAllTime, actual.AllTime);
            Assert.Equal(expectedDimension, actual.Dimension);
            Assert.Equal(expectedAllProdMinutes, actual.ProductionMinutes);
            Assert.Equal(expectedAllPauseMinutes, actual.PauseMinutes);
        }
    }
}
