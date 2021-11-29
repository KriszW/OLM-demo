using OLM.Services.TCO.API.Extensions;
using OLM.Services.TCO.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Extensions.TCOData
{
    public class CalculateGoodProductsMethodTests
    {
        [Fact]
        public void Calculate()
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

            var expected = 7310;

            //Act
            var actual = tcoData.CalculateGoodProducts();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
