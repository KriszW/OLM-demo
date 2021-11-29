using FluentValidation.TestHelper;
using OLM.Services.Tram.API.Validations;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Validators.TramFetchRequest
{
    public class EndValidationTests
    {
        [Fact]
        public void End_Should_Be_Valid()
        {
            var validator = new TramFetchRequestValidator();
            var validationResult = validator.Validate(new TramFetchRequestViewModel { End = DateTime.Now.AddDays(1), Start = DateTime.Now });

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public void End_Should_Be_Later_ThanStart()
        {
            var validator = new TramFetchRequestValidator();
            var validationResult = validator.Validate(new TramFetchRequestViewModel { End = DateTime.Now.AddDays(-1), Start = DateTime.Now });

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }
    }
}
