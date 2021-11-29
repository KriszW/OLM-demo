using FluentValidation;
using OLM.Services.CategoryBulbs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Validators
{
    public class ItemnumberCategoryModelValidator : AbstractValidator<ItemnumberCategoryModel>
    {
        public ItemnumberCategoryModelValidator()
        {
            RuleFor(m => m.Itemnumber)
                .NotEmpty().WithMessage("A cikkszám nem lehet üres")
                .Matches(@"^([a-zA-Z0-9])+(-)?(([a-zA-Z0-9]))?").WithMessage("A cikkszám érvénytelen, a cikkszámban csak számokat és betűket tartalmazhat és maximum egy '-' karaktert. (pl: 10000-AB)");
        }
    }
}
