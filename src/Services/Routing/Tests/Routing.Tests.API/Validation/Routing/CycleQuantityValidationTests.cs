using FluentValidation.TestHelper;
using OLM.Services.Routing.API.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Validation.Routing
{
    public class CycleQuantityValidationTests
    {
        [Fact]
        public void Dimension_Should_Be_Valid()
        {
            var validator = new RoutingModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.CycleQuantityPerMinute, 0.1);
        }

        [Fact]
        public void Dimension_Should_Be_InValid_For_0()
        {
            var validator = new RoutingModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.CycleQuantityPerMinute, 0);
        }

        [Fact]
        public void Dimension_Should_Be_InValid_For_Minus()
        {
            var validator = new RoutingModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.CycleQuantityPerMinute, -0.1);
        }
    }
}
