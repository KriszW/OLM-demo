using Moq;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.OneMachine;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.ApiGateWays.SPA.Web.Gtw.SPA.Tests.Aggregator.BundleMachine
{
    public class GetDataForMachineMethodTests
    {
        [Fact]
        public async Task GetData_ShouldSuccess()
        {
            //Arrange
            var machineID = "3";

            var mockedLatestBundle = new Mock<ILatestBundleWithBundlePriceForMachineAggregator>();
            mockedLatestBundle.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(new LatestBundleViewModel());
            var mockedDailyBundle = new Mock<IDailyBundlesWithBundlePriceForMachineAggregator>();
            mockedDailyBundle.Setup(m => m.FetchDailyBundles(machineID)).ReturnsAsync(new DailyMachineDataViewModel());
            var mockedWeeklyBundle = new Mock<IWeeklyBundlesWithBundleIDForMachineAggregator>();
            mockedWeeklyBundle.Setup(m => m.FetchWeeklyBundles(machineID)).ReturnsAsync(new WeeklyMachineDataViewModel());

            //Act
            var aggregator = new BundleMachineAggregator(mockedLatestBundle.Object, mockedDailyBundle.Object, mockedWeeklyBundle.Object, default, default);
            var result = await aggregator.GetDataForMachine(machineID);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model.Latest);
            Assert.NotNull(result.Model.Daily);
            Assert.NotNull(result.Model.Weekly);

            mockedLatestBundle.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedDailyBundle.Verify(m => m.FetchDailyBundles(machineID), Times.Once());
            mockedWeeklyBundle.Verify(m => m.FetchWeeklyBundles(machineID), Times.Once());
        }

        [Fact]
        public async Task GetData_LatestIsNull()
        {
            //Arrange
            var machineID = "3";

            var mockedLatestBundle = new Mock<ILatestBundleWithBundlePriceForMachineAggregator>();
            mockedLatestBundle.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(default(LatestBundleViewModel));
            var mockedDailyBundle = new Mock<IDailyBundlesWithBundlePriceForMachineAggregator>();
            mockedDailyBundle.Setup(m => m.FetchDailyBundles(machineID)).ReturnsAsync(new DailyMachineDataViewModel());
            var mockedWeeklyBundle = new Mock<IWeeklyBundlesWithBundleIDForMachineAggregator>();
            mockedWeeklyBundle.Setup(m => m.FetchWeeklyBundles(machineID)).ReturnsAsync(new WeeklyMachineDataViewModel());

            //Act
            var aggregator = new BundleMachineAggregator(mockedLatestBundle.Object, mockedDailyBundle.Object, mockedWeeklyBundle.Object, default, default);
            var result = await aggregator.GetDataForMachine(machineID);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Model.Latest);
            Assert.NotNull(result.Model.Daily);
            Assert.NotNull(result.Model.Weekly);

            mockedLatestBundle.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedDailyBundle.Verify(m => m.FetchDailyBundles(machineID), Times.Once());
            mockedWeeklyBundle.Verify(m => m.FetchWeeklyBundles(machineID), Times.Once());
        }

        [Fact]
        public async Task GetData_DailyIsNull()
        {
            //Arrange
            var machineID = "3";

            var mockedLatestBundle = new Mock<ILatestBundleWithBundlePriceForMachineAggregator>();
            mockedLatestBundle.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(new LatestBundleViewModel());
            var mockedDailyBundle = new Mock<IDailyBundlesWithBundlePriceForMachineAggregator>();
            mockedDailyBundle.Setup(m => m.FetchDailyBundles(machineID)).ReturnsAsync(default(DailyMachineDataViewModel));
            var mockedWeeklyBundle = new Mock<IWeeklyBundlesWithBundleIDForMachineAggregator>();
            mockedWeeklyBundle.Setup(m => m.FetchWeeklyBundles(machineID)).ReturnsAsync(new WeeklyMachineDataViewModel());

            //Act
            var aggregator = new BundleMachineAggregator(mockedLatestBundle.Object, mockedDailyBundle.Object, mockedWeeklyBundle.Object, default, default);
            var result = await aggregator.GetDataForMachine(machineID);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model.Latest);
            Assert.Null(result.Model.Daily);
            Assert.NotNull(result.Model.Weekly);

            mockedLatestBundle.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedDailyBundle.Verify(m => m.FetchDailyBundles(machineID), Times.Once());
            mockedWeeklyBundle.Verify(m => m.FetchWeeklyBundles(machineID), Times.Once());
        }

        [Fact]
        public async Task GetData_WeeklyIsNull()
        {
            //Arrange
            var machineID = "3";

            var mockedLatestBundle = new Mock<ILatestBundleWithBundlePriceForMachineAggregator>();
            mockedLatestBundle.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(new LatestBundleViewModel());
            var mockedDailyBundle = new Mock<IDailyBundlesWithBundlePriceForMachineAggregator>();
            mockedDailyBundle.Setup(m => m.FetchDailyBundles(machineID)).ReturnsAsync(new DailyMachineDataViewModel());
            var mockedWeeklyBundle = new Mock<IWeeklyBundlesWithBundleIDForMachineAggregator>();
            mockedWeeklyBundle.Setup(m => m.FetchWeeklyBundles(machineID)).ReturnsAsync(default(WeeklyMachineDataViewModel));

            //Act
            var aggregator = new BundleMachineAggregator(mockedLatestBundle.Object, mockedDailyBundle.Object, mockedWeeklyBundle.Object, default, default);
            var result = await aggregator.GetDataForMachine(machineID);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model.Latest);
            Assert.NotNull(result.Model.Daily);
            Assert.Null(result.Model.Weekly);

            mockedLatestBundle.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedDailyBundle.Verify(m => m.FetchDailyBundles(machineID), Times.Once());
            mockedWeeklyBundle.Verify(m => m.FetchWeeklyBundles(machineID), Times.Once());
        }
    }
}
