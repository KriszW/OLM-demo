using FluentValidation;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Validators
{
    public class IntegerValidator : AbstractValidator<int>
    {
        public IntegerValidator()
        {
            RuleSet("PageIndex", () => {

                RuleFor(m => m).ValidatePaginationPageIndex();

            });

            RuleSet("PageSize", () => {

                RuleFor(m => m).ValidatePaginationPageSize();

            });

            RuleSet("ItemnumberCategorysID", () =>
            {
                RuleFor(m => m)
                    .GreaterThan(0).WithMessage("A paraméter cikk kategória azonosító nem lehet kisebb mint 1");
            });
        }
    }
}
