using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Services.RawTCOBundleSettingsAggregator
{
    public class CalculateWeightedExpectedTCOMethodTests
    {
        [Fact]
        public void CalculateWeighted_ShouldReturnResult()
        {
            //Arrange
            var bundles = new List<TCODataModel>
            {
                new TCODataModel
                {
                    BundleID = "bundle1",
                    RawMaterialItemNumber = "5x25",
                    VendorID = "1000",
                    Primary = 3000,
                    Secondary = 400,
                    Volume = 5.235
                },
                new TCODataModel
                {
                    BundleID = "bundle2",
                    RawMaterialItemNumber = "5x25",
                    VendorID = "1100",
                    Primary = 2000,
                    Secondary = 0,
                    Volume = 3.235
                }
                ,
                new TCODataModel
                {
                    BundleID = "bundle3",
                    RawMaterialItemNumber = "5x75",
                    VendorID = "1100",
                    Primary = 6000,
                    Secondary = 700,
                    Volume = 7.235
                }
            };

            var settings = new List<TCOValueSettingsModel>
            {
                new TCOValueSettingsModel
                {
                    ExpectedTCOValue = 1.2,
                    RawMaterialItemNumber = "5x75"
                },
                new TCOValueSettingsModel
                {
                    ExpectedTCOValue = 1.1,
                    RawMaterialItemNumber = "5x25"
                },
            };

            var expectedValue = 1.1460681311684175;

            //Act
            var service = new RawTCOBundleSettingsAggregatorService();
            var actual = service.CalculateWeightedExpectedTCO(bundles, settings);

            //Assert
            Assert.Equal(expectedValue, actual);
        }

        [Fact]
        public void CalculateWeighted_ShouldReturnDefaultForEmptyBundles()
        {
            //Arrange
            var bundles = default(IEnumerable<TCODataModel>);
            var settings = new List<TCOValueSettingsModel>
            {
                new TCOValueSettingsModel
                {
                    ExpectedTCOValue = 1.2,
                    RawMaterialItemNumber = "5x75"
                },
                new TCOValueSettingsModel
                {
                    ExpectedTCOValue = 1.1,
                    RawMaterialItemNumber = "5x25"
                },
            };

            //Act
            var service = new RawTCOBundleSettingsAggregatorService();
            var actual = service.CalculateWeightedExpectedTCO(bundles, settings);

            //Assert
            Assert.Equal(0.0,actual);
        }

        [Fact]
        public void CalculateWeighted_ShouldReturnDefaultForEmptySettings()
        {
            //Arrange
            var bundles = new List<TCODataModel>
            {
                new TCODataModel
                {
                    BundleID = "bundle1",
                    RawMaterialItemNumber = "5x25",
                    VendorID = "1000",
                    Primary = 3000,
                    Secondary = 400,
                    Volume = 5.235
                },
                new TCODataModel
                {
                    BundleID = "bundle2",
                    RawMaterialItemNumber = "5x25",
                    VendorID = "1100",
                    Primary = 2000,
                    Secondary = 0,
                    Volume = 3.235
                }
                ,
                new TCODataModel
                {
                    BundleID = "bundle3",
                    RawMaterialItemNumber = "5x75",
                    VendorID = "1100",
                    Primary = 6000,
                    Secondary = 700,
                    Volume = 7.235
                }
            };

            var settings = default(IEnumerable<TCOValueSettingsModel>);

            //Act
            var service = new RawTCOBundleSettingsAggregatorService();
            var actual = service.CalculateWeightedExpectedTCO(bundles, settings);

            //Assert
            Assert.Equal(0.0, actual);
        }
    }
}
