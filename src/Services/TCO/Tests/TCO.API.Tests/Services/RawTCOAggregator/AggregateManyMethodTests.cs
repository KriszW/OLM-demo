using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Services.RawTCOAggregator
{
    public class AggregateManyMethodTests
    {
        [Fact]
        public async Task Aggregate_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoSettingsRepo = new TCOSettingsRepository(dbContext);
            var tcoCalcProviderService = new EuroTCOCalculationProvider();
            var tcoCalcService = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProviderService);
            var tcoSettingsService = new RawTCOBundleSettingsAggregatorService();

            var model = new List<RawTCOQueryDataViewModel>
            {
                new RawTCOQueryDataViewModel
                {
                    BundleIDs = new List<string>
                    {
                        "bundle1",
                    },
                    ItemNumber = "5x25",
                    Price = 60000,
                    VendorID = "102340"
                },
                new RawTCOQueryDataViewModel
                {
                    BundleIDs = new List<string>
                    {
                        "bundle2",
                    },
                    ItemNumber = "5x25",
                    Price = 70000,
                    VendorID = "103341"
                },
            };

            var expectedPrice = 51326.58999696015;
            var expectedMaxDiff = 0.1;
            var expectedTCO = 1.2;

            //Act
            var service = new RawTCOAggregatorService(tcoDataRepo, tcoSettingsRepo, tcoCalcService, tcoSettingsService);
            var actual = await service.Aggregate(model);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPrice, actual.CalculatedValue);
            Assert.Equal(expectedTCO, actual.ExpectedTCOValue);
            Assert.Equal(expectedMaxDiff, actual.MaximumDifference);
        }

        [Fact]
        public async Task Aggregate_ShouldReturn0ForNotExistingBundleID()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoSettingsRepo = new TCOSettingsRepository(dbContext);
            var tcoCalcProviderService = new EuroTCOCalculationProvider();
            var tcoCalcService = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProviderService);
            var tcoSettingsService = new RawTCOBundleSettingsAggregatorService();

            var model = new List<RawTCOQueryDataViewModel>
            {
                new RawTCOQueryDataViewModel
                {
                    BundleIDs = new List<string>
                    {
                        "bundle1",
                    },
                    ItemNumber = "5x25",
                    Price = 60000,
                    VendorID = "102340"
                },
                new RawTCOQueryDataViewModel
                {
                    BundleIDs = new List<string>
                    {
                        "bundle1000",
                    },
                    ItemNumber = "5x25",
                    Price = 60000,
                    VendorID = "102340"
                },
            };

            var expectedPrice = 45600.0;
            var expectedMaxDiff = 0.1;
            var expectedTCO = 1.2;

            //Act
            var service = new RawTCOAggregatorService(tcoDataRepo, tcoSettingsRepo, tcoCalcService, tcoSettingsService);
            var actual = await service.Aggregate(model);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedPrice, actual.CalculatedValue);
            Assert.Equal(expectedTCO, actual.ExpectedTCOValue);
            Assert.Equal(expectedMaxDiff, actual.MaximumDifference);
        }

        [Fact]
        public async Task Aggregate_ShouldReturnDefaultForModelIsNull()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoSettingsRepo = new TCOSettingsRepository(dbContext);
            var tcoCalcProviderService = new EuroTCOCalculationProvider();
            var tcoCalcService = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProviderService);
            var tcoSettingsService = new RawTCOBundleSettingsAggregatorService();

            var model = default(IEnumerable<RawTCOQueryDataViewModel>);

            //Act
            var service = new RawTCOAggregatorService(tcoDataRepo, tcoSettingsRepo, tcoCalcService, tcoSettingsService);
            var actual = await service.Aggregate(model);

            //Assert
            Assert.Null(actual);
        }
    }
}
