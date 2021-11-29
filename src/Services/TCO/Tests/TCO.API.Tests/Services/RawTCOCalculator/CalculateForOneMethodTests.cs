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

namespace OLM.Services.TCO.API.Tests.Services.RawTCOCalculator
{
    public class CalculateForOneMethodTests
    {
        [Fact]
        public async Task Calculate_ShouldReturnResult()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoCalcProvider = new EuroTCOCalculationProvider();

            var model = new RawTCOQueryDataViewModel
            {
                VendorID = "",
                Price = 67000,
                ItemNumber = "5x25",
                BundleIDs = new List<string>
                {
                    "bundle1"
                }
            };

            var expectedValue = 50920.0;

            //Act
            var service = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProvider);
            var actual = await service.Calculate(model);

            //Assert
            Assert.Equal(expectedValue, actual);
        }

        [Fact]
        public async Task Calculate_ShouldReturnDefaultForModelIsNull()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoCalcProvider = new EuroTCOCalculationProvider();

            var model = default(RawTCOQueryDataViewModel);

            var expectedValue = default(double);

            //Act
            var service = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProvider);
            var actual = await service.Calculate(model);

            //Assert
            Assert.Equal(expectedValue, actual);
        }

        [Fact]
        public async Task Calculate_ShouldReturnDefaultForBundleIDsIsNull()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoCalcProvider = new EuroTCOCalculationProvider();

            var model = new RawTCOQueryDataViewModel
            {
                VendorID = "",
                Price = 67000,
                ItemNumber = "5x25",
                BundleIDs = default
            };

            var expectedValue = default(double);

            //Act
            var service = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProvider);
            var actual = await service.Calculate(model);

            //Assert
            Assert.Equal(expectedValue, actual);
        }

        [Fact]
        public async Task Calculate_ShouldReturnDefaultForEmptyBundleIDs()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoCalcProvider = new EuroTCOCalculationProvider();

            var model = new RawTCOQueryDataViewModel
            {
                VendorID = "",
                Price = 67000,
                ItemNumber = "5x25",
                BundleIDs = new List<string>
                {
                }
            };

            var expectedValue = default(double);

            //Act
            var service = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProvider);
            var actual = await service.Calculate(model);

            //Assert
            Assert.Equal(expectedValue, actual);
        }

        [Fact]
        public async Task Calculate_ShouldReturnDefaultForUnknownBundleID()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);
            var tcoDataRepo = new TCODataRepository(dbContext);
            var tcoCalcProvider = new EuroTCOCalculationProvider();

            var model = new RawTCOQueryDataViewModel
            {
                VendorID = "",
                Price = 67000,
                ItemNumber = "5x25",
                BundleIDs = new List<string>
                {
                    "sadwd213"
                }
            };

            var expectedValue = default(double);

            //Act
            var service = new RawTCOCalculatorService(tcoDataRepo, tcoCalcProvider);
            var actual = await service.Calculate(model);

            //Assert
            Assert.Equal(expectedValue, actual);
        }
    }
}
