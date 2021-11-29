using Moq;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.BundlePrices;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations.SummarizedMachines;
using OLM.Services.SharedBases.APIErrors;

namespace OLM.ApiGateWays.SPA.Web.Gtw.SPA.Tests.Aggregator.BundleMachine.Summarized.Weekly
{
    public class FetchSummarizedWeeklyMethodTests
    {
        [Fact]
        public async Task Fetch_ShouldBeOk()
        {
            //Arrange
            var bundleIDs = new string[] { "bundle1", "bundle2" };
            var expectedPrices = new List<RawTCOQueryDataViewModel> { new RawTCOQueryDataViewModel() };

            var mockedFetchMachine = new Mock<IFetchSummarizedBundleService>();
            mockedFetchMachine.Setup(m => m.FetchWeekly()).ReturnsAsync(new SummarizedDataForMachinesViewModel(0, 0, 0, 0, bundleIDs));
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateAVGTCO(expectedPrices)).ReturnsAsync(new BundleTCOAPIResponseViewModel(0, 0, 0));
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleIDs)).ReturnsAsync(expectedPrices);

            //Act
            var aggregator = new SummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object);
            var result = (await aggregator.FetchSummarizedWeekly()).Value;

            //Assert
            Assert.NotNull(result);

            mockedFetchMachine.Verify(m => m.FetchWeekly(), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateAVGTCO(expectedPrices), Times.Once());
            mockedBundlePrice.Verify(m => m.Fetch(bundleIDs), Times.Once());
        }

        [Fact]
        public async Task Fetch_BundleReturnedNull()
        {
            //Arrange
            var bundleIDs = new string[] { "bundle1", "bundle2" };
            var expectedPrices = new List<RawTCOQueryDataViewModel>();

            var mockedFetchMachine = new Mock<IFetchSummarizedBundleService>();
            mockedFetchMachine.Setup(m => m.FetchWeekly()).ReturnsAsync(new APIError());
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateAVGTCO(expectedPrices)).ReturnsAsync(new BundleTCOAPIResponseViewModel(0, 0, 0));
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleIDs)).ReturnsAsync(expectedPrices);

            //Act
            var aggregator = new SummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object);
            var result = (await aggregator.FetchSummarizedWeekly()).Value;

            //Assert
            Assert.IsType<APIError>(result);

            mockedFetchMachine.Verify(m => m.FetchWeekly(), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateAVGTCO(expectedPrices), Times.Never());
            mockedBundlePrice.Verify(m => m.Fetch(bundleIDs), Times.Never());
        }

        [Fact]
        public async Task Fetch_BundlePricesReturnedNull()
        {
            //Arrange
            var bundleIDs = new string[] { "bundle1", "bundle2" };
            var expectedPrices = default(List<RawTCOQueryDataViewModel>);

            var mockedFetchMachine = new Mock<IFetchSummarizedBundleService>();
            mockedFetchMachine.Setup(m => m.FetchWeekly()).ReturnsAsync(new SummarizedDataForMachinesViewModel(0, 0, 0, 0, bundleIDs));
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateAVGTCO(expectedPrices)).ReturnsAsync(default(BundleTCOAPIResponseViewModel));
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleIDs)).ReturnsAsync(new APIError());

            //Act
            var aggregator = new SummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object);
            var result = (await aggregator.FetchSummarizedWeekly()).Value;

            //Assert
            Assert.IsType<APIError>(result);

            mockedFetchMachine.Verify(m => m.FetchWeekly(), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateAVGTCO(expectedPrices), Times.Never());
            mockedBundlePrice.Verify(m => m.Fetch(bundleIDs), Times.Once());
        }

        [Fact]
        public async Task Fetch_TCOReturnedNull()
        {
            //Arrange
            var bundleIDs = new string[] { "bundle1", "bundle2" };
            var expectedPrices = new List<RawTCOQueryDataViewModel> { new RawTCOQueryDataViewModel() };

            var mockedFetchMachine = new Mock<IFetchSummarizedBundleService>();
            mockedFetchMachine.Setup(m => m.FetchWeekly()).ReturnsAsync(new SummarizedDataForMachinesViewModel(0, 0, 0, 0, bundleIDs));
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateAVGTCO(expectedPrices)).ReturnsAsync(new APIError());
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleIDs)).ReturnsAsync(expectedPrices);

            //Act
            var aggregator = new SummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object);
            var result = (await aggregator.FetchSummarizedWeekly()).Value;

            //Assert
            Assert.IsType<APIError>(result);

            mockedFetchMachine.Verify(m => m.FetchWeekly(), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateAVGTCO(expectedPrices), Times.Once());
            mockedBundlePrice.Verify(m => m.Fetch(bundleIDs), Times.Once());
        }
    }
}
