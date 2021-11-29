using FluentValidation;
using OLM.Services.MoneyExchangeRate.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Validation
{
    public class ExchangeRateValidator : AbstractValidator<ExchangeRateModel>
    {
        public ExchangeRateValidator()
        {
            RuleFor(m => m.DestISOCode)
                .NotEmpty().WithMessage("A valuta kódja nem lehet üres")
                .Length(3, 3).WithMessage("A valuta kódja csak 3 karakter hosszú lehet");

            RuleFor(m => m.Rate)
                .GreaterThan(0).WithMessage("Az átváltási értéknek nagyobbnak kell lennie mint 0");
        }
    }
}
