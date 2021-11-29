using FluentValidation.TestHelper;
using OLM.Services.Bundles.Prices.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Validator.BundlePriceFromItemNumber
{
    public class RawMatNumberValidationTests
    {
        [Fact]
        public void MatNum_Should_Be_Valid()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.RawMaterialItemNumber, "151322671");
        }

        [Fact]
        public void MatNum_Should_Be_InValid_For_EmptyString()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.RawMaterialItemNumber, "");
        }

        [Fact]
        public void MatNum_Should_Be_InValid_For_NotValidMaterialNum()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.RawMaterialItemNumber, "nemvalidmaterial@1234");
        }
    }
}
