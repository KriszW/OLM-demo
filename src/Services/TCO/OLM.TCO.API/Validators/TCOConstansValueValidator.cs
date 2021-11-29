using FluentValidation;
using OLM.Services.TCO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Validators
{
    public class TCOConstansValueValidator : AbstractValidator<TCOValueSettingsModel>
    {
        public TCOConstansValueValidator()
        {
            RuleFor(m => m.RawMaterialItemNumber)
                .NotEmpty().WithMessage("A cikk szám nem lehet üres")
                .Matches(@"^[0-9]+$").WithMessage("A cikkszám érvénytelen, csak számokat tartalmazhat");

            RuleFor(m => m.ExpectedTCOValue)
                .GreaterThanOrEqualTo(0).WithMessage("Az elvárt TCO értéknek nem lehet kisebb mint 0");

            RuleFor(m => m.MaximumDifference)
                .GreaterThanOrEqualTo(0).WithMessage("A maximum eltérés nem lehet kisebb mint 0");
        }
    }
}
