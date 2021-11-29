using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Validation
{
    public class MachineNameValidator : AbstractValidator<string>
    {
        public MachineNameValidator()
        {
            RuleFor(a => a)
                .NotEmpty().WithMessage($"A gép név nem lehet üres");
        }
    }
}
