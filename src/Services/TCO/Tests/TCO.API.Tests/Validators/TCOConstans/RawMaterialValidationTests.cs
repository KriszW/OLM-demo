using FluentValidation.TestHelper;
using OLM.Services.TCO.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Validators.TCOConstans
{
    public class RawMaterialValidationTests
    {
        [Fact]
        public void MatNum_Should_Be_Valid()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.RawMaterialItemNumber, "151322671");
        }

        [Fact]
        public void MatNum_Should_Be_InValid_For_EmptyString()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldHaveValidationErrorFor(m => m.RawMaterialItemNumber, "");
        }

        [Fact]
        public void MatNum_Should_Be_InValid_For_NotValidMaterialNum()
        {
            var validator = new TCOConstansValueValidator();
            validator.ShouldHaveValidationErrorFor(m => m.RawMaterialItemNumber, "nemvalidmaterial@1234");
        }
    }
}
