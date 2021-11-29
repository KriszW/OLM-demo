using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Validators
{
    public class BundleIDValidator : AbstractValidator<string>
    {
        public BundleIDValidator()
        {
            RuleFor(m => m)
                .NotEmpty().WithMessage("A rakat azonosító nem lehet üres")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("A rakat azonosító érvénytelen karaktereket tartalmaz, a rakat azonosító csak számokból és betűkből állhat");
        }
    }
}
