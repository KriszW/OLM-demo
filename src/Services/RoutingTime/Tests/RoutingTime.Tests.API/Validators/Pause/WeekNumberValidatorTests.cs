using FluentValidation.TestHelper;
using OLM.Services.RoutingTime.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Validators.Pause
{
    public class WeekNumberValidatorTests
    {
        [Fact]
        public void WeekNumber_Should_BeValid()
        {
            var value = 26;
            var validator = new PauseModelValidator();

            validator.ShouldNotHaveValidationErrorFor(m => m.WeekNumber, value);
        }

        [Fact]
        public void WeekNumber_Should_BeInvalid_For_LessThan1()
        {
            var value = 0;
            var validator = new PauseModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.WeekNumber, value);
        }

        [Fact]
        public void WeekNumber_Should_BeInvalid_For_MoreThan53()
        {
            var value = 55;
            var validator = new PauseModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.WeekNumber, value);
        }
    }
}
