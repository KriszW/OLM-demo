using FluentValidation.TestHelper;
using OLM.Services.Bundles.Prices.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Validator.BundlePrice
{
    public class CurrencyValidationTests
    {
        [Fact]
        public void Currency_ShouldBeValid()
        {
            var value = "HUF";
            var validator = new BundlePriceModelValidator();

            validator.ShouldNotHaveValidationErrorFor(m => m.Currency, value);
        }

        [Fact]
        public void Currency_ShouldBeInValid()
        {
            var value = "";
            var validator = new BundlePriceModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.Currency, value);
        }

        [Fact]
        public void Currency_ShouldBeInValidTooLong()
        {
            var value = "HUFG";
            var validator = new BundlePriceModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.Currency, value);
        }

        [Fact]
        public void Currency_ShouldBeInValidTooShort()
        {
            var value = "HU";
            var validator = new BundlePriceModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.Currency, value);
        }
    }
}
