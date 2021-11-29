using OLM.Services.Bundles.API.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Services.EndDate
{
    public class GetEndDateOfToday
    {
        [Fact]
        public void CalculateEndDateForToday()
        {
            //Arrange
            var expectedDate = DateTime.Now.Date;
            var expecetedTimeSpan = new TimeSpan(23, 59, 59);

            //Act
            var provider = new EndDateFromTodayProvider();
            var actual = provider.GetEndOfTheDay();

            //Assert
            Assert.Equal(expectedDate, actual.Date);
            Assert.Equal(expecetedTimeSpan, actual.TimeOfDay);
        }
    }
}
