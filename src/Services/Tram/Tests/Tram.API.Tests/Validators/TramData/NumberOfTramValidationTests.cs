using FluentValidation.TestHelper;
using OLM.Services.Tram.API.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Validators.TramData
{
    public class NumberOfTramValidationTests
    {
        [Fact]
        public void NumberOFTram_Should_Be_Valid()
        {
            var validator = new TramDataModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.NumberOfTrams, 1);
        }

        [Fact]
        public void NumberOFTram_Should_Be_InValid_For_Minus1()
        {
            var validator = new TramDataModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.NumberOfTrams, -1);
        }
    }
}
