using FluentValidation;
using OLM.Services.Tram.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Validations
{
    public class TramDimensionValidator : AbstractValidator<TramDimensionModel>
    {
        public TramDimensionValidator()
        {
            RuleFor(m => m.Dimension)
                .NotEmpty().WithMessage("A dimenzió nem lehet üres")
                .Matches(@"([0-9])+( \* )+([0-9])+(XL)?").WithMessage("A dimenzió formátuma nem megfelelő, a dimenzió ebből áll: Magasság * Szélesség (25 * 150)");
        }
    }
}
