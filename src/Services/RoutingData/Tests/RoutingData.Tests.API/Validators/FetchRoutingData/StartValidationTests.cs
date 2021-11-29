using OLM.Services.RoutingData.API.Validation;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingData.Tests.API.Validators.FetchRoutingData
{
    public class StartValidationTests
    {
        [Fact]
        public void Start_Should_Be_Valid()
        {
            var validator = new FetchRoutingDataValidator();
            var validationResult = validator.Validate(new FetchRoutingDataRequestViewModel { End = DateTime.Now.AddDays(1), Start = DateTime.Now, MachineName = "1" });

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public void Start_Should_Be_Later_ThanStart()
        {
            var validator = new FetchRoutingDataValidator();
            var validationResult = validator.Validate(new FetchRoutingDataRequestViewModel { End = DateTime.Now, Start = DateTime.Now.AddDays(1), MachineName = "1" });

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }
    }
}
