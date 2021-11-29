using FluentValidation.TestHelper;
using OLM.Services.Target.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Target.API.Tests.Validators.WasteTarget
{
    public class TargetValidationTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(0.1121)]
        public void Target_Should_Be_Valid(double value)
        {
            var validator = new WasteTargetDataModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.Target, value);
        }

        [Theory]
        [InlineData(-1.0)]
        [InlineData(1.01)]
        [InlineData(-0.99)]
        public void Target_OutsideRange(double value)
        {
            var validator = new WasteTargetDataModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Target, value);
        }
    }
}
