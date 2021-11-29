using Moq;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.SummarizedMachines;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.ApiGateWays.SPA.Web.Gtw.SPA.Tests.Aggregator.BundleMachine
{
    public class GetDataForMachinesMethodTests
    {
        [Fact]
        public async Task GetData_ShouldSuccess()
        {
            //Arrange
            var mockedDailyBundle = new Mock<ISummarizedDailyBundlesWithBundlePriceForMachinesAggreagator>();
            mockedDailyBundle.Setup(m => m.FetchSummarizedDaily()).ReturnsAsync(new DailySummarizedViewModel());
            var mockedWeeklyBundle = new Mock<ISummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator>();
            mockedWeeklyBundle.Setup(m => m.FetchSummarizedWeekly()).ReturnsAsync(new WeeklySummarizedViewModel());

            //Act
            var aggregator = new BundleMachineAggregator(default, default, default, mockedDailyBundle.Object, mockedWeeklyBundle.Object);
            var result = await aggregator.GetDataForMachines();

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model.Daily);
            Assert.NotNull(result.Model.Weekly);

            mockedDailyBundle.Verify(m => m.FetchSummarizedDaily(), Times.Once());
            mockedWeeklyBundle.Verify(m => m.FetchSummarizedWeekly(), Times.Once());
        }

        [Fact]
        public async Task GetData_DailyIsNull()
        {
            //Arrange
            var mockedDailyBundle = new Mock<ISummarizedDailyBundlesWithBundlePriceForMachinesAggreagator>();
            mockedDailyBundle.Setup(m => m.FetchSummarizedDaily()).ReturnsAsync(default(DailySummarizedViewModel));
            var mockedWeeklyBundle = new Mock<ISummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator>();
            mockedWeeklyBundle.Setup(m => m.FetchSummarizedWeekly()).ReturnsAsync(new WeeklySummarizedViewModel());

            //Act
            var aggregator = new BundleMachineAggregator(default, default, default, mockedDailyBundle.Object, mockedWeeklyBundle.Object);
            var result = await aggregator.GetDataForMachines();

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Model.Daily);
            Assert.NotNull(result.Model.Weekly);

            mockedDailyBundle.Verify(m => m.FetchSummarizedDaily(), Times.Once());
            mockedWeeklyBundle.Verify(m => m.FetchSummarizedWeekly(), Times.Once());
        }

        [Fact]
        public async Task GetData_WeeklyIsNull()
        {
            //Arrange
            var mockedDailyBundle = new Mock<ISummarizedDailyBundlesWithBundlePriceForMachinesAggreagator>();
            mockedDailyBundle.Setup(m => m.FetchSummarizedDaily()).ReturnsAsync(new DailySummarizedViewModel());
            var mockedWeeklyBundle = new Mock<ISummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator>();
            mockedWeeklyBundle.Setup(m => m.FetchSummarizedWeekly()).ReturnsAsync(default(WeeklySummarizedViewModel));

            //Act
            var aggregator = new BundleMachineAggregator(default, default, default, mockedDailyBundle.Object, mockedWeeklyBundle.Object);
            var result = await aggregator.GetDataForMachines();

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model.Daily);
            Assert.Null(result.Model.Weekly);

            mockedDailyBundle.Verify(m => m.FetchSummarizedDaily(), Times.Once());
            mockedWeeklyBundle.Verify(m => m.FetchSummarizedWeekly(), Times.Once());
        }
    }
}
