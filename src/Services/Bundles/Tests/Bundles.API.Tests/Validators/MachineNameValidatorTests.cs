using FluentValidation.TestHelper;
using OLM.Services.Bundles.API.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.API.Tests.Validators
{
    public class MachineNameValidatorTests
    {
        [Fact]
        public void TestMachineName_ShouldBeValid()
        {
            var value = "1";
            var validator = new MachineNameValidation();

            validator.ShouldNotHaveValidationErrorFor(m => m, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValid()
        {
            var value = "";
            var validator = new MachineNameValidation();

            validator.ShouldHaveValidationErrorFor(m => m, value);
        }
    }
}
