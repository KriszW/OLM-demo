using FluentValidation.TestHelper;
using OLM.Services.MoneyExchangeRate.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Validation.ExchangeRateModel
{
    public class DestISOCodeValidationTests
    {
        [Fact]
        public void TestMachineName_ShouldBeValid()
        {
            var value = "HUF";
            var validator = new ExchangeRateValidator();

            validator.ShouldNotHaveValidationErrorFor(m => m.DestISOCode, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValid()
        {
            var value = "";
            var validator = new ExchangeRateValidator();

            validator.ShouldHaveValidationErrorFor(m => m.DestISOCode, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValidTooLong()
        {
            var value = "HUFG";
            var validator = new ExchangeRateValidator();

            validator.ShouldHaveValidationErrorFor(m => m.DestISOCode, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValidTooShort()
        {
            var value = "HU";
            var validator = new ExchangeRateValidator();

            validator.ShouldHaveValidationErrorFor(m => m.DestISOCode, value);
        }
    }
}
