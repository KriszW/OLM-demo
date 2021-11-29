using FluentValidation.TestHelper;
using OLM.Services.Tram.API.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Validators.TramData
{
    public class NumberOfLamellaValidationTests
    {
        [Fact]
        public void NumberOFLamella_Should_Be_Valid()
        {
            var validator = new TramDataModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.NumberOfLamella, 1);
        }

        [Fact]
        public void NumberOFLamella_Should_Be_InValid_For_Minus1()
        {
            var validator = new TramDataModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.NumberOfLamella, -1);
        }
    }
}
