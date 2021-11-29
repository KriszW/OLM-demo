using Microsoft.AspNetCore.Mvc;
using OLM.Services.TCO.API.Extensions;
using OLM.Services.TCO.API.Models;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Extensions.TCOData
{
    public class CreateTCOViewModelMethodTests
    {
        [Fact]
        public void CreateModelFromPrice()
        {
            //Arrange
            var tcoData = new TCODataModel()
            {
                BundleID = "bundle1",
                RawMaterialItemNumber = "25x5",
                Primary = 3000,
                Secondary = 4310,
                Volume = 6003,
                ID = 1,
                VendorID = "1",
            };

            var price = new BundlePriceViewModel("25x5", "1", 4300);

            //Act
            var actual = tcoData.CreateTCOViewModel(price);

            //Assert
            Assert.Equal(tcoData.Volume, actual.AllVolume);
            Assert.Equal((double)price.Price, actual.Price);
            Assert.Equal((tcoData.Primary + tcoData.Secondary), actual.GoodProducts);
        }

        [Fact]
        public void CreateModelFromDouble()
        {
            //Arrange
            var tcoData = new TCODataModel()
            {
                BundleID = "bundle1",
                RawMaterialItemNumber = "25x5",
                Primary = 3000,
                Secondary = 4310,
                Volume = 6003,
                ID = 1
            };

            var price = 60000.0;

            //Act
            var actual = tcoData.CreateTCOViewModel(price);

            //Assert
            Assert.Equal(tcoData.Volume, actual.AllVolume);
            Assert.Equal((double)price, actual.Price);
            Assert.Equal((tcoData.Primary + tcoData.Secondary), actual.GoodProducts);
        }

        [Fact]
        public void CreateModelFromDecimal()
        {
            //Arrange
            var tcoData = new TCODataModel()
            {
                BundleID = "bundle1",
                RawMaterialItemNumber = "25x5",
                Primary = 3000,
                Secondary = 4310,
                Volume = 6003,
                ID = 1
            };

            var price = 60000M;

            //Act
            var actual = tcoData.CreateTCOViewModel(price);

            //Assert
            Assert.Equal(tcoData.Volume, actual.AllVolume);
            Assert.Equal((double)price, actual.Price);
            Assert.Equal((tcoData.Primary + tcoData.Secondary), actual.GoodProducts);
        }
    }
}
