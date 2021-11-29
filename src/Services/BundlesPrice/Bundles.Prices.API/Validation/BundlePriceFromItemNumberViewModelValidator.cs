using FluentValidation;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Validation
{
    public class BundlePriceFromItemNumberViewModelValidator : AbstractValidator<BundlePriceFromItemNumberViewModel>
    {
        public BundlePriceFromItemNumberViewModelValidator()
        {
            RuleFor(m => m.RawMaterialItemNumber)
                .NotEmpty().WithMessage("A cikk szám nem lehet üres");
            //.Matches(@"^[0-9]+$").WithMessage("A cikkszám érvénytelen, csak számokat tartalmazhat");

            RuleFor(m => m.VendorID)
                .NotEmpty().WithMessage("A beszállító kód nem lehet üres");
            //.Matches(@"^[0-9]+$").WithMessage("A beszállító kód érvénytelen, csak számokat tartalmazhat");
        }
    }
}
