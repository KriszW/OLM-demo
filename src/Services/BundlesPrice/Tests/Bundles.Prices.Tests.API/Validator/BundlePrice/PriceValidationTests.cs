using FluentValidation.TestHelper;
using OLM.Services.Bundles.Prices.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Validator.BundlePrice
{
    public class PriceValidationTests
    {
        [Fact]
        public void Price_Should_Be_Valid()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.Price, 1500);
        }

        [Fact]
        public void Price_Should_Be_Valid_For_Price0()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.Price, 0);
        }

        [Fact]
        public void Price_Should_Be_InValid_For_MinusPrice()
        {
            var validator = new BundlePriceModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Price, -1);
        }
    }
}
