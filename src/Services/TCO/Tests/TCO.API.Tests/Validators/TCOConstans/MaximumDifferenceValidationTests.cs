using FluentValidation.TestHelper;
using OLM.Services.TCO.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Validators.TCOConstans
{
    public class MaximumDifferenceValidationTests
    {
        [Fact]
        public void MaximumDifference_Should_Be_Valid()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.MaximumDifference, 0.1);
        }

        [Fact]
        public void MaximumDifference_Should_Be_Valid_For_MaximumDifference0()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.MaximumDifference, 0);
        }

        [Fact]
        public void MaximumDifference_Should_Be_InValid_For_MinusMaximumDifference()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldHaveValidationErrorFor(m => m.MaximumDifference, -0.1);
        }
    }
}
