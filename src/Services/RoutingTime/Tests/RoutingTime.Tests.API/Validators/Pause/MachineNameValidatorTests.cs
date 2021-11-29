using FluentValidation.TestHelper;
using OLM.Services.RoutingTime.API.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Validators.Pause
{
    public class MachineNameValidatorTests
    {
        [Fact]
        public void TestMachineName_ShouldBeValid()
        {
            var value = "1";
            var validator = new PauseModelValidator();

            validator.ShouldNotHaveValidationErrorFor(m => m.MachineName, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValid()
        {
            var value = "";
            var validator = new PauseModelValidator();

            validator.ShouldHaveValidationErrorFor(m => m.MachineName, value);
        }
    }
}
