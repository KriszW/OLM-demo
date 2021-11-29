using FluentValidation.TestHelper;
using OLM.Services.Identity.API.Validators;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Identity.API.Tests.Validation.Login
{
    public class LoginUserNameValidatorTests
    {
        [Fact]
        public void UserName_Should_Be_Valid()
        {
            var validator = new LoginValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.UserName, "user");
        }

        [Fact]
        public void UserName_Should_Be_InValid_For_Empty()
        {
            var validator = new LoginValidator();
            validator.ShouldHaveValidationErrorFor(m => m.UserName, "");
        }
    }
}
