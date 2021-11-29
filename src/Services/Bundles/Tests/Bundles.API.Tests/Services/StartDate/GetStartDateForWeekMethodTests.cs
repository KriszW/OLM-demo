using OLM.Services.Bundles.API.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Services.StartDate
{
    public class GetStartDateForWeekMethodTests
    {
        [Fact]
        public void CalculateStarDateForWeek()
        {
            //Arrange
            var expectedDay = DayOfWeek.Monday;
            var expecetedTimeSpan = new TimeSpan(6, 0, 0);

            //Act
            var provider = new StartDateFromTodayProvider();
            var actual = provider.GetStartDateForWeek();

            //Assert
            Assert.Equal(expectedDay, actual.DayOfWeek);
            Assert.Equal(expecetedTimeSpan, actual.TimeOfDay);
        }
    }
}
