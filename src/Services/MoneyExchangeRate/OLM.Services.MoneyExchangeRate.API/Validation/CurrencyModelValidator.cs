using FluentValidation;
using OLM.Services.MoneyExchangeRate.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Validation
{
    public class CurrencyModelValidator : AbstractValidator<CurrencyModel>
    {
        public CurrencyModelValidator()
        {
            RuleFor(m => m.ISOCode)
                .NotEmpty().WithMessage("A valuta kódja nem lehet üres")
                .Length(3,3).WithMessage("A valuta kódja csak 3 karakter hosszú lehet");
        }
    }
}
