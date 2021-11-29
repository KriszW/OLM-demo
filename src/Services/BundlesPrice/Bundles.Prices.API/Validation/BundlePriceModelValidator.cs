using FluentValidation;
using OLM.Services.Bundles.Prices.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Validation
{
    public class BundlePriceModelValidator : AbstractValidator<BundlePriceModel>
    {
        public BundlePriceModelValidator()
        {
            RuleFor(m => m.RawMaterialItemNumber)
                .NotEmpty().WithMessage("A cikk szám nem lehet üres");
            //.Matches(@"^[0-9]+$").WithMessage("A cikkszám érvénytelen, csak számokat tartalmazhat");

            RuleFor(m => m.VendorID)
                .NotEmpty().WithMessage("A beszállító kód nem lehet üres");
            //.Matches(@"^[0-9]+$").WithMessage("A beszállító kód érvénytelen, csak számokat tartalmazhat");

            RuleFor(m => m.Price)
                .GreaterThanOrEqualTo(0).WithMessage("A rakat ára nem lehet kisebb mint 0");

            RuleFor(m => m.Currency)
                .NotEmpty().WithMessage("A valuta kódja nem lehet üres")
                .Length(3, 3).WithMessage("A valuta kódja csak 3 karakter hosszú lehet");
        }
    }
}
