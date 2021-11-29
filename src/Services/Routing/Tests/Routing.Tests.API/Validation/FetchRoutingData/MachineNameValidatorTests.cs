using FluentValidation.TestHelper;
using OLM.Services.Routing.API.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Validation.FetchRoutingData
{
    public class MachineNameValidatorTests
    {
        [Fact]
        public void TestMachineName_ShouldBeValid()
        {
            var value = "1";
            var validator = new RoutingRequestValidator();

            validator.ShouldNotHaveValidationErrorFor(m => m.MachineName, value);
        }

        [Fact]
        public void TestMachineName_ShouldBeInValid()
        {
            var value = "";
            var validator = new RoutingRequestValidator();

            validator.ShouldHaveValidationErrorFor(m => m.MachineName, value);
        }
    }
}
