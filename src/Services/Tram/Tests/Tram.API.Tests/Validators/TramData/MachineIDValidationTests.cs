using FluentValidation.TestHelper;
using OLM.Services.Tram.API.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Validators.TramData
{
    public class MachineIDValidationTests
    {
        [Fact]
        public void MachineID_Should_Be_Valid()
        {
            var validator = new TramDataModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.MachineID, "1");
        }

        [Fact]
        public void MachineID_Should_Be_InValid_For_EmptyString()
        {
            var validator = new TramDataModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.MachineID, "");
        }
    }
}
