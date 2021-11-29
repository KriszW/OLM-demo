using FluentValidation;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Validations
{
    public class TramFetchRequestValidator : AbstractValidator<TramFetchRequestViewModel>
    {
        public TramFetchRequestValidator()
        {
            RuleFor(m => m.Start).Must((model, start) => start < model.End).WithMessage("A kezdő időpontnak kisebbnek kell lennie mint a vég időpontnak");

            RuleFor(m => m.End).Must((model, end) => end > model.Start).WithMessage("A vég időpontnak nagyobbnak kell lennie mint a kezdő időpontnak");
        }
    }
}
