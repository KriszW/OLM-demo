using FluentValidation;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(m => m.Password)
                .NotEmpty().WithMessage("Az jelszó nem lehet üres");

            RuleFor(m => m.UserName)
                .NotEmpty().WithMessage("A felhasználónév nem lehet üres");
        }
    }
}
