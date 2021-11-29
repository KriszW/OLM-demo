using FluentValidation.TestHelper;
using OLM.Services.MoneyExchangeRate.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Validation.CurrencyModel
{
    public class ISOCodeValidationTests
    {
        [Fact]
        public void TestMachineName_ShouldBeValid()
        {
            var value = "HUF";
            var validator = new CurrencyModelValidator();

            validator.ShouldNotHaveValidationErrorFor(m => m.ISOCode, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValid()
        {
            var value = "";
            var validator = new CurrencyModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.ISOCode, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValidTooLong()
        {
            var value = "HUFG";
            var validator = new CurrencyModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.ISOCode, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValidTooShort()
        {
            var value = "HU";
            var validator = new CurrencyModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.ISOCode, value);
        }
    }
}
