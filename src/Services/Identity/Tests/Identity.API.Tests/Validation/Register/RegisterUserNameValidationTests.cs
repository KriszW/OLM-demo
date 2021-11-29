using FluentValidation.TestHelper;
using OLM.Services.Identity.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Identity.API.Tests.Validation.Register
{
    public class RegisterUserNameValidationTests
    {
        [Fact]
        public void UserName_Should_Be_Valid()
        {
            var validator = new RegisterValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.UserName, "kriszw2001");
        }

        [Fact]
        public void UserName_Should_Be_InValid_For_Empty()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.UserName, "");
        }

        [Fact]
        public void UserName_Should_Be_InValid_For_120Character()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.UserName, "asdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdas");
        }

        [Fact]
        public void UserName_Should_Be_InValid_For_NotUserName()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.UserName, "kriszw2001@._ ");
        }
    }
}
