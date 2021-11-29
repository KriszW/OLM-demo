using FluentValidation.TestHelper;
using OLM.Services.TCO.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Validators.TCOConstans
{
    public class ExpectedTCOValueValidationTests
    {
        [Fact]
        public void ExpectedTCOValue_Should_Be_Valid()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.ExpectedTCOValue, 1500);
        }

        [Fact]
        public void ExpectedTCOValue_Should_Be_Valid_For_ExpectedTCOValue0()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.ExpectedTCOValue, 0);
        }

        [Fact]
        public void ExpectedTCOValue_Should_Be_InValid_For_MinusExpectedTCOValue()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldHaveValidationErrorFor(m => m.ExpectedTCOValue, -1);
        }
    }
}
