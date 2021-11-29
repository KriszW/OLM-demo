using FluentValidation;
using OLM.Services.Routing.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Validation
{
    public class RoutingModelValidator : AbstractValidator<RoutingModel>
    {
        public RoutingModelValidator()
        {
            RuleFor(m => m.Dimension)
                .NotEmpty().WithMessage("A dimenzió nem lehet üres")
                .Matches(@"([0-9])+( \* )+([0-9])+(XL)?").WithMessage("A dimenzió formátuma nem megfelelő, a dimenzió ebből áll: Magasság * Szélesség (25 * 150)");

            RuleFor(m => m.CycleQuantityPerMinute)
                .GreaterThan(0).WithMessage("A routing ciklus értéknek nagyobbnak kell lennie mint 0");

        }
    }
}
