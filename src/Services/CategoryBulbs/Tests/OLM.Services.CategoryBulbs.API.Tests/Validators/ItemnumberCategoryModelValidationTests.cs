using FluentValidation.TestHelper;
using OLM.Services.CategoryBulbs.API.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.CategoryBulbs.API.Tests.Validators
{
    public class ItemnumberCategoryModelValidationTests
    {
        [Fact]
        public void Validate_Should_Be_Valid_Itemnumber()
        {
            var validator = new ItemnumberCategoryModelValidator();
            validator.ShouldNotHaveValidationErrorFor(m => m.Itemnumber, "151235678");
        }

        [Fact]
        public void Validate_Should_Be_InValid_Empty_Itemnumber()
        {
            var validator = new ItemnumberCategoryModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Itemnumber, "").WithErrorMessage("A cikkszám nem lehet üres");
        }

        [Fact]
        public void Validate_Should_Be_InValid_ContainsLetter_Itemnumber()
        {
            var validator = new ItemnumberCategoryModelValidator();
            validator.ShouldHaveValidationErrorFor(m => m.Itemnumber, "1512dsa678").WithErrorMessage("A cikkszám érvénytelen, csak számokat tartalmazhat");
        }
    }
}
