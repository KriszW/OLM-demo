using FluentValidation.TestHelper;
using OLM.Services.Identity.API.Validators;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Identity.API.Tests.Validation.Register
{
    public class RegisterEmailValidationTests
    {
        [Fact]
        public void Email_Should_Be_Valid()
        {
            var validator = new RegisterValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.Email, "test@gmail.com");
        }

        [Fact]
        public void Email_Should_Be_InValid_For_Empty()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Email, "");
        }

        [Fact]
        public void Email_Should_Be_InValid_For_120Character()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Email, "asdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdasasdasdasdasdasdasdasdasdasdasdasdsadasdasdasdas");
        }

        [Fact]
        public void Email_Should_Be_InValid_For_NotEmail()
        {
            var validator = new RegisterValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Email, "iamnotemail_:asdqwr:123ca");
        }
    }
}
