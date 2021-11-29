using FluentValidation.TestHelper;
using OLM.Services.Routing.API.Validation;
using OLM.Shared.Models.Routing.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Validation.FetchRoutingData
{
    public class EndValidationTests
    {
        [Fact]
        public void End_Should_Be_Valid()
        {
            var validator = new RoutingRequestValidator();
            var validationResult = validator.Validate(new RoutingRequestViewModel { End = DateTime.Now.AddDays(1), Start = DateTime.Now, MachineName = "1" });

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public void End_Should_Be_Later_ThanStart()
        {
            var validator = new RoutingRequestValidator();
            var validationResult = validator.Validate(new RoutingRequestViewModel { End = DateTime.Now.AddDays(-1), Start = DateTime.Now, MachineName = "1" });

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }
    }
}
