using FluentValidation;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Validation
{
    public class FetchRoutingTimeRequestValidator : AbstractValidator<FetchRoutingRequestTimeViewModel>
    {
        public FetchRoutingTimeRequestValidator()
        {
            RuleFor(m => m.MachineName).NotEmpty().WithMessage("A gépnév nem lehet üres");

            RuleFor(m => m.Start).Must((model, start) => start < model.End).WithMessage("A kezdő időpontnak kisebbnek kell lennie mint a vég időpontnak");

            RuleFor(m => m.End).Must((model, end) => end > model.Start).WithMessage("A vég időpontnak nagyobbnak kell lennie mint a kezdő időpontnak");
        }
    }
}
