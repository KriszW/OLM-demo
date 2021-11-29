using FluentValidation;
using OLM.Services.RoutingTime.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Validation
{
    public class PauseModelValidator : AbstractValidator<PauseModel>
    {
        public PauseModelValidator()
        {
            RuleFor(m => m.Start)
                .Must((model, start) => start.Date == model.End.Date || start.Date.AddDays(1) == model.End.Date).WithMessage("A kezdő és a vég időpont között maximum egy nap eltérés lehet")
                .Must((model, start) => start < model.End).WithMessage("A kezdő időpontnak kisebbnek kell lennie mint a vég időpontnak");

            RuleFor(m => m.End).Must((model, end) => end > model.Start).WithMessage("A vég időpontnak nagyobbnak kell lennie mint a kezdő időpontnak");

            RuleFor(m => m.MachineName).NotEmpty().WithMessage("A gépnév nem lehet üres");

            RuleFor(m => m.WeekNumber)
                .GreaterThan(0).WithMessage("Az évben a hétnek a száma nem lehet kisebb mint 1")
                .LessThanOrEqualTo(53).WithMessage("Az évben a hétnek a száma nem lehet nagyobb mint 53");
        }
    }
}
