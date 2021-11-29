using FluentValidation.TestHelper;
using OLM.Services.Bundles.Prices.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Validator.BundlePriceFromItemNumber
{
    public class VendorIDValidationTests
    {
        [Fact]
        public void MatNum_Should_Be_Valid()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.VendorID, "151322671");
        }

        [Fact]
        public void MatNum_Should_Be_InValid_For_EmptyString()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.VendorID, "");
        }

        [Fact]
        public void MatNum_Should_Be_InValid_For_NotValidMaterialNum()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.VendorID, "nemvalidmaterial@1234");
        }
    }
}
