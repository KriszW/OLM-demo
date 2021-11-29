using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Services.StartDate
{
    public class GetStartDateForDayMethodTests
    {
        [Fact]
        public void CalculateStartDateForToday()
        {
            //Arrange
            var expectedDate = DateTime.Now.Date;
            var expecetedTimeSpan = new TimeSpan(6, 0, 0);

            //Act
            var provider = new StartDateFromTodayProvider();
            var actual = provider.GetStartDateForDay();

            //Assert
            Assert.Equal(expectedDate, actual.Date);
            Assert.Equal(expecetedTimeSpan, actual.TimeOfDay);
        }
    }
}
