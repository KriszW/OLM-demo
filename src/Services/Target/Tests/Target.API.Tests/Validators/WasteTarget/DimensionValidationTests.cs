using FluentValidation.TestHelper;
using OLM.Services.Target.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Target.API.Tests.Validators.WasteTarget
{
    public class DimensionValidationTests
    {
        [Theory]
        [InlineData("1 * 1")]
        [InlineData("1 * 1XL")]
        [InlineData("25 * 75")]
        [InlineData("25 * 75XL")]
        public void Dimension_Should_Be_Valid(string dimension)
        {
            var validator = new WasteTargetDataModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.Dimension, dimension);
        }

        [Theory]
        [InlineData("")]
        [InlineData("21 3")]
        [InlineData("213ad")]
        [InlineData("213y213")]
        [InlineData("213x213")]
        public void Dimension_Should_Be_InValid_For_InvalidDimension(string dimension)
        {
            var validator = new WasteTargetDataModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Dimension, dimension);
        }
    }
}
