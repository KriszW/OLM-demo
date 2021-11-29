using Moq;
using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Services.Repositories.Abstractions;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.API.Services.Services.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using OLM.Shared.Models.Routing.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Response;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Services.Routing
{
    public class CalculateMethodTests
    {
        [Fact]
        public async Task Calculate_ShouldBeOkay()
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

            var expectedCount = 2;

            var expectedExpRoutingValueFor25x75 = 40503.13775999562;
            var expectedActualValueFor25x75 = 42228;
            var expectedExpRoutingValueFor19x75 = 8232.107902287293;
            var expectedActualValueFor19x75 = 7700;

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
            var mockedRoutingDataRepo = new Mock<IRoutingDataRepository>();;
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

            //Act
            var service = new RoutingService(routingRepo, mockedRoutingTimeRepo.Object, mockedRoutingDataRepo.Object);
            var actual = await service.Calculate(model);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(model.Start, actual.Start);
            Assert.Equal(model.End, actual.End);
            Assert.Equal(expectedCount, actual.Data.Count());

            var dim25x75 = actual.Data.FirstOrDefault(m => m.Dimension == "25 * 75");
            var dim19x75 = actual.Data.FirstOrDefault(m => m.Dimension == "19 * 75");

            Assert.NotNull(dim25x75);
            Assert.NotNull(dim19x75);

            Assert.Equal(expectedExpRoutingValueFor25x75, dim25x75.ExpectedRouting);
            Assert.Equal(expectedActualValueFor25x75, dim25x75.ActualRouting);
            Assert.Equal(expectedExpRoutingValueFor19x75, dim19x75.ExpectedRouting);
            Assert.Equal(expectedActualValueFor19x75, dim19x75.ActualRouting);
        }
    }
}
