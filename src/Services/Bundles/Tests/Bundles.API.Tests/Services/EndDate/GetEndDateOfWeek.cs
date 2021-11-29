using OLM.Services.Bundles.API.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Services.EndDate
{
    public class GetEndDateOfWeek
    {
        [Fact]
        public void CalculateEndDateForWeek()
        {
            //Arrange
            var expectedDay = DayOfWeek.Sunday;
            var expecetedTimeSpan = new TimeSpan(23, 59, 59);

            //Act
            var provider = new EndDateFromTodayProvider();
            var actual = provider.GetEndOfTheWeek();

            //Assert
            Assert.Equal(expectedDay, actual.DayOfWeek);
            Assert.Equal(expecetedTimeSpan, actual.TimeOfDay);
        }
    }
}
