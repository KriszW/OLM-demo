using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Validators
{
    public class StringEnumerableValidator : AbstractValidator<IEnumerable<string>>
    {
        public StringEnumerableValidator()
        {
            RuleSet("BundleIDs", () =>
            {
                RuleFor(m => m)
                    .NotEmpty().WithMessage("A rakat azonosítók nem lehet üres");
            });

            RuleSet("ItemNumbers", () =>
            {
                RuleFor(m => m)
                    .NotEmpty().WithMessage("A cikkszámok nem lehet üres");
            });
        }
    }
}
