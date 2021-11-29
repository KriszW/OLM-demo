using FluentValidation;
using OLM.Services.Tram.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Validations
{
    public class TramDataModelValidator : AbstractValidator<TramDataModel>
    {
        public TramDataModelValidator()
        {
            RuleFor(m => m.MachineID)
                .NotEmpty().WithMessage("A gép neve nem lehet üres");

            RuleFor(m => m.NumberOfLamella)
                .GreaterThanOrEqualTo(0).WithMessage("A kitett lamellák száma nem lehet kisebb mint 0");

            RuleFor(m => m.NumberOfTrams)
                .GreaterThanOrEqualTo(0).WithMessage("A csillék száma nem lehet kisebb mint 0");

            RuleFor(m => m.Dimension).SetValidator(new TramDimensionValidator());
        }
    }
}
