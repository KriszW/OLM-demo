using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Validation
{
    public class MachineNameValidation : AbstractValidator<string>
    {
        public MachineNameValidation()
        {
            RuleFor(machineName => machineName)
                .NotEmpty().WithMessage("A gép név nem lehet üres");
        }
    }
}
