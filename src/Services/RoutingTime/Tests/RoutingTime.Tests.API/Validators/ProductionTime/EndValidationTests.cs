using FluentValidation.TestHelper;
using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Validation;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Validators.ProductionTime
{
    public class EndValidationTests
    {
        [Fact]
        public void End_Should_Be_Valid()
        {
            var validator = new ProductionTimeValidator();
            var validationResult = validator.Validate(new ProductionTimeModel { End = DateTime.Now.AddMinutes(1), Start = DateTime.Now, MachineName = "1", WeekNumber = 23 });

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public void End_Should_Be_Valid_ForOnePlusDay()
        {
            var validator = new ProductionTimeValidator();
            var validationResult = validator.Validate(new ProductionTimeModel { End = DateTime.Now.AddDays(1), Start = DateTime.Now, MachineName = "1", WeekNumber = 23 });

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public void End_Should_Be_Invalid_For_MoreThan1Day()
        {
            var validator = new ProductionTimeValidator();
            var validationResult = validator.Validate(new ProductionTimeModel { End = DateTime.Now.AddDays(2), Start = DateTime.Now, MachineName = "1", WeekNumber = 23 });

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }

        [Fact]
        public void End_Should_Be_Later_ThanStart()
        {
            var validator = new ProductionTimeValidator();
            var validationResult = validator.Validate(new ProductionTimeModel { End = DateTime.Now.AddMinutes(-1), Start = DateTime.Now, MachineName = "1", WeekNumber = 23 });

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }
    }
}
