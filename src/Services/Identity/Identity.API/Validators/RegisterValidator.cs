using FluentValidation;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterNewUserViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty().WithMessage("Az e-mail cím nem lehet üres")
                .MaximumLength(100).WithMessage("Az e-mail cím nem lehet hosszabb mint {MaxLength} karakter, te {TotalLength} karaktert adtál meg")
                .EmailAddress().WithMessage("Az e-mail cím formátuma nem valid");

            RuleFor(m => m.Password)
                .NotEmpty().WithMessage("Az jelszó nem lehet üres")
                .MaximumLength(50).WithMessage("A jelszó nem lehet hosszabb mint {MaxLength} karakter, te {TotalLength} karaktert adtál meg")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("A jelszó érvénytelen karaktereket tartalmaz, a jelszó csak számokat és betűket tartalmazhat");

            RuleFor(m => m.UserName)
                .NotEmpty().WithMessage("A felhasználónév nem lehet üres")
                .MaximumLength(50).WithMessage("A felhasználónév nem lehet hosszabb mint {MaxLength} karakter, te {TotalLength} karaktert adtál meg")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("A felhasználónév érvénytelen karaktereket tartalmaz, a felhasználónév csak számokat és betűket tartalmazhat");

            RuleFor(m => m.ConfirmPassword)
                .Equal(m => m.Password).WithMessage("A két jelszó nem egyezik");
        }
    }
}
