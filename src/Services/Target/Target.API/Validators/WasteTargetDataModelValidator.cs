using FluentValidation;
using OLM.Services.Target.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Validators
{
    public class WasteTargetDataModelValidator : AbstractValidator<WasteTargetDataModel>
    {
        public WasteTargetDataModelValidator()
        {
            RuleFor(m => m.Target)
                .InclusiveBetween(0, 1).WithMessage("A Target értéknek 0 és 1 között kell lennie");

            RuleFor(m => m.Dimension)
                .NotEmpty().WithMessage("A dimenzió nem lehet üres")
                .Matches(@"([0-9])+( \* )+([0-9])+(XL)?").WithMessage("A dimenzió formátuma nem megfelelő, a dimenzió ebből áll: Magasság * Szélesség (25 * 150)");
        }
    }
}
