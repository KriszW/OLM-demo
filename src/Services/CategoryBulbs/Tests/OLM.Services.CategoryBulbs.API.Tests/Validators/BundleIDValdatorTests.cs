using FluentValidation.TestHelper;
using OLM.Services.CategoryBulbs.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.CategoryBulbs.API.Tests.Validators
{
    public class BundleIDValdatorTests
    {
        [Fact]
        public void BundleID_Should_Be_Valid()
        {
            var validator = new BundleIDValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m, "Bundle1");
        }

        [Fact]
        public void BundleID_Should_Be_InValid_For_UnderScore()
        {
            var validator = new BundleIDValidator();
            validator.ShouldHaveValidationErrorFor(m => m, "Bundle1_");
        }

        [Fact]
        public void BundleID_Should_Be_InValid_For_Empty()
        {
            var validator = new BundleIDValidator();
            validator.ShouldHaveValidationErrorFor(m => m, "");
        }

        [Fact]
        public void BundleID_Should_Be_InValid_For_Space()
        {
            var validator = new BundleIDValidator();
            validator.ShouldHaveValidationErrorFor(m => m, "Bund le1");
        }

        [Fact]
        public void BundleID_Should_Be_InValid_For_Any_Special_Character()
        {
            var validator = new BundleIDValidator();
            validator.ShouldHaveValidationErrorFor(m => m, "Bundle1.+!%$");
        }
    }
}
