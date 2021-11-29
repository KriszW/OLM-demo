using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Services.TCO.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Services.TCOCalculationProvider
{
    public class CalculateMethodTests
    {
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void Calc(TCOCalculationViewModel model, double expected)
        {
            //Arrange
            var tcoCalc = new EuroTCOCalculationProvider();

            //Act
            var actual = tcoCalc.Calculate(model);

            //Assert
            Assert.Equal(expected, actual);
        }

        public static object[][] CreateTestData() => new object[][]
        {
            new object[] { default, 0.0 },
            new object[] { new TCOCalculationViewModel(1000,1.2,1231), 0.974817221770918 },
        };
    }
}
