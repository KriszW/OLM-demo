using FluentValidation.TestHelper;
using OLM.Services.Identity.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Identity.API.Tests.Validation.Register
{
    public class RegisterPasswordValidationTests
    {
        [Fact]
        public void Password_Should_Be_Valid()
        {
            var validator = new RegisterValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.Password, "titok123");
        }

        [Fact]
        public void Password_Should_Be_InValid_For_Empty()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Password, "");
        }

        [Fact]
        public void Password_Should_Be_InValid_For_120Character()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Password, "asdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdas");
        }

        [Fact]
        public void Password_Should_Be_InValid_For_NotPassword()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Password, "titok1234@._ ");
        }
    }
}
