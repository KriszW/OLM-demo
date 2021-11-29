using FluentValidation.TestHelper;
using OLM.Services.Identity.API.Validators;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Identity.API.Tests.Validation.Login
{
    public class LoginPasswordValidatorTests
    {
        [Fact]
        public void Password_Should_Be_Valid()
        {
            var validator = new LoginValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.Password, "titok");
        }

        [Fact]
        public void Password_Should_Be_InValid_For_Empty()
        {
            var validator = new LoginValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Password, "");
        }
    }
}
