using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Validators
{
    public class StringValidator : AbstractValidator<string>
    {
        public StringValidator()
        {
            RuleSet("BundleID", () =>
            {
                RuleFor(m => m)
                    .NotEmpty().WithMessage("A rakat azonosító nem lehet üres")
                    .Matches(@"^[a-zA-Z0-9]+$").WithMessage("A rakat azonosító érvénytelen karaktereket tartalmaz, a rakat azonosító csak számokból és betűkből állhat");
            });

            RuleSet("ItemNumber", () =>
            {
                RuleFor(m => m)
                    .NotEmpty().WithMessage("A cikkszám nem lehet üres")
                    .Matches(@"^[0-9]+$").WithMessage("A cikkszám érvénytelen, csak számokat tartalmazhat");
            });
        }
    }
}
