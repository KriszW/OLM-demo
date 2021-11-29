using Moq;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OLM.Shared.Models.Bundles.APIResponses;
using OLM.Shared.Models.CategoryBulbs.APIResponses;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.BundlePrices;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.CategoryBulbs;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations.OneMachine;
using OLM.Services.SharedBases.APIErrors;

namespace OLM.ApiGateWays.SPA.Web.Gtw.SPA.Tests.Aggregator.BundleMachine.OneMachine.Latest
{
    public class FetchLatestBundleMethodTests
    {
        [Fact]
        public async Task Fetch_ShouldBeOk()
        {
            //Arrange
            var machineID = "3";
            var expectedPrice = new RawTCOQueryDataViewModel();
            var bundleID = "bundle1";

            var mockedFetchMachine = new Mock<IFetchOneMachinesBundleService>();
            mockedFetchMachine.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(new BundleAPIResponseViewModel() { BundleID = bundleID });
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateTCO(expectedPrice)).ReturnsAsync(new BundleTCOAPIResponseViewModel(0, 0, 0));
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleID)).ReturnsAsync(expectedPrice);
            var mockedValidator = new Mock<IValidateOneBundleService>();
            mockedValidator.Setup(m => m.ValidateBundle(bundleID)).ReturnsAsync(new List<ValidationResult>());
            
            
            //Act
            var aggregator = new LatestBundleWithBundlePriceForMachineAggregator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object, mockedValidator.Object);
            var result = (await aggregator.FetchLatestBundle(machineID)).Value;

            //Assert
            Assert.NotNull(result);

            mockedFetchMachine.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateTCO(expectedPrice), Times.Once());
            mockedValidator.Verify(m => m.ValidateBundle(bundleID), Times.Once());
            mockedBundlePrice.Verify(m => m.Fetch(bundleID), Times.Once());
        }

        [Fact]
        public async Task Fetch_BundleReturnedNull()
        {
            //Arrange
            var machineID = "3";
            var expectedPrice = new RawTCOQueryDataViewModel();
            var bundleID = "bundle1";

            var mockedFetchMachine = new Mock<IFetchOneMachinesBundleService>();
            mockedFetchMachine.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(new APIError());
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateTCO(expectedPrice)).ReturnsAsync(new BundleTCOAPIResponseViewModel(0, 0, 0));
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleID)).ReturnsAsync(expectedPrice);
            var mockedValidator = new Mock<IValidateOneBundleService>();
            mockedValidator.Setup(m => m.ValidateBundle(bundleID)).ReturnsAsync(new List<ValidationResult>());

            //Act
            var aggregator = new LatestBundleWithBundlePriceForMachineAggregator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object, mockedValidator.Object);
            var result = (await aggregator.FetchLatestBundle(machineID)).Value;

            //Assert
            Assert.IsType<APIError>(result);

            mockedFetchMachine.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateTCO(expectedPrice), Times.Never());
            mockedValidator.Verify(m => m.ValidateBundle(bundleID), Times.Never());
            mockedBundlePrice.Verify(m => m.Fetch(bundleID), Times.Never());
        }

        [Fact]
        public async Task Fetch_BundlePricesReturnedNull()
        {
            //Arrange
            var machineID = "3";
            var expectedPrice = default(RawTCOQueryDataViewModel);

            var bundleID = "bundle1";

            var mockedFetchMachine = new Mock<IFetchOneMachinesBundleService>();
            mockedFetchMachine.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(new BundleAPIResponseViewModel() { BundleID = bundleID });
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateTCO(expectedPrice)).ReturnsAsync(new BundleTCOAPIResponseViewModel(0,0,0));
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleID)).ReturnsAsync(new APIError());
            var mockedValidator = new Mock<IValidateOneBundleService>();
            mockedValidator.Setup(m => m.ValidateBundle(bundleID)).ReturnsAsync(new List<ValidationResult>());

            //Act
            var aggregator = new LatestBundleWithBundlePriceForMachineAggregator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object, mockedValidator.Object);
            var result = (await aggregator.FetchLatestBundle(machineID)).Value;

            //Assert
            Assert.IsType<APIError>(result);

            mockedFetchMachine.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateTCO(expectedPrice), Times.Never());
            mockedValidator.Verify(m => m.ValidateBundle(bundleID), Times.Never());
            mockedBundlePrice.Verify(m => m.Fetch(bundleID), Times.Once());
        }

        [Fact]
        public async Task Fetch_TCOReturnedNull()
        {
            //Arrange
            var machineID = "3";
            var expectedPrice = new RawTCOQueryDataViewModel();

            var bundleID = "bundle1";

            var mockedFetchMachine = new Mock<IFetchOneMachinesBundleService>();
            mockedFetchMachine.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(new BundleAPIResponseViewModel() { BundleID = bundleID });
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateTCO(expectedPrice)).ReturnsAsync(new APIError());
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleID)).ReturnsAsync(expectedPrice);
            var mockedValidator = new Mock<IValidateOneBundleService>();
            mockedValidator.Setup(m => m.ValidateBundle(bundleID)).ReturnsAsync(new List<ValidationResult>());

            //Act
            var aggregator = new LatestBundleWithBundlePriceForMachineAggregator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object, mockedValidator.Object);
            var result = (await aggregator.FetchLatestBundle(machineID)).Value;

            //Assert
            Assert.IsType<APIError>(result);

            mockedFetchMachine.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateTCO(expectedPrice), Times.Once());
            mockedValidator.Verify(m => m.ValidateBundle(bundleID), Times.Once());
            mockedBundlePrice.Verify(m => m.Fetch(bundleID), Times.Once());
        }

        [Fact]
        public async Task Fetch_ValidationReturnedNull()
        {
            //Arrange
            var machineID = "3";
            var expectedPrice = new RawTCOQueryDataViewModel();

            var bundleID = "bundle1";

            var mockedFetchMachine = new Mock<IFetchOneMachinesBundleService>();
            mockedFetchMachine.Setup(m => m.FetchLatestBundle(machineID)).ReturnsAsync(new BundleAPIResponseViewModel() { BundleID = bundleID });
            var mockedTCOCalc = new Mock<IFetchRawTCOCalculatorService>();
            mockedTCOCalc.Setup(m => m.CalculateTCO(expectedPrice)).ReturnsAsync(default(BundleTCOAPIResponseViewModel));
            var mockedBundlePrice = new Mock<IFetchBundlePriceService>();
            mockedBundlePrice.Setup(m => m.Fetch(bundleID)).ReturnsAsync(expectedPrice);
            var mockedValidator = new Mock<IValidateOneBundleService>();
            mockedValidator.Setup(m => m.ValidateBundle(bundleID)).ReturnsAsync(new APIError());

            //Act
            var aggregator = new LatestBundleWithBundlePriceForMachineAggregator(mockedFetchMachine.Object, mockedTCOCalc.Object, mockedBundlePrice.Object, mockedValidator.Object);
            var result = (await aggregator.FetchLatestBundle(machineID)).Value;

            //Assert
            Assert.IsType<APIError>(result);

            mockedFetchMachine.Verify(m => m.FetchLatestBundle(machineID), Times.Once());
            mockedTCOCalc.Verify(m => m.CalculateTCO(expectedPrice), Times.Once());
            mockedValidator.Verify(m => m.ValidateBundle(bundleID), Times.Once());
            mockedBundlePrice.Verify(m => m.Fetch(bundleID), Times.Once());
        }
    }
}
