using FluentValidation.TestHelper;
using OLM.Services.MoneyExchangeRate.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Validation.ExchangeRateModel
{
    public class RateValidationTests
    {
        [Fact]
        public void TestMachineName_ShouldBeValid()
        {
            var value = 100.2M;
            var validator = new ExchangeRateValidator();

            validator.ShouldNotHaveValidationErrorFor(m => m.Rate, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValid()
        {
            var value = 0;
            var validator = new ExchangeRateValidator();

            validator.ShouldHaveValidationErrorFor(m => m.Rate, value);
        }
    }
}
