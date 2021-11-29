using FluentValidation.TestHelper;
using OLM.Services.Identity.API.Validators;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Identity.API.Tests.Validation.Register
{
    public class RegisterConfirmPasswordValidationTests
    {
        [Fact]
        public void ConfirmPassword_Should_Be_Valid()
        {
            var validator = new RegisterValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.ConfirmPassword, new RegisterNewUserViewModel() { Password = "titok", ConfirmPassword = "titok"});
        }

        [Fact]
        public void ConfirmPassword_Should_Be_InValid_For_Empty()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.ConfirmPassword, "");
        }

        [Fact]
        public void ConfirmPassword_Should_Be_InValid_For_NotEqualTo_Password()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.ConfirmPassword, new RegisterNewUserViewModel() { Password = "titok", ConfirmPassword = "nemtitok" });
        }
    }
}
