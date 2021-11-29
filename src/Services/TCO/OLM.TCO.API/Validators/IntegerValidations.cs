using FluentValidation;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Validators
{
    public class IntegerValidations : AbstractValidator<int>
    {
        public IntegerValidations()
        {
            RuleSet("PageIndex", () => {

                RuleFor(m => m).ValidatePaginationPageIndex();

            });

            RuleSet("PageSize", () => {

                RuleFor(m => m).ValidatePaginationPageSize();

            });

            RuleSet("BundlePriceID", () =>
            {
                RuleFor(m => m)
                    .GreaterThan(0).WithMessage("A rakat ár azonosítónak nagyobbnak kell lennie mint 0");
            });

            RuleSet("TCOSettingsID", () =>
            {
                RuleFor(m => m)
                    .GreaterThan(0).WithMessage("A TCO konstans beállítás azonosítónak nagyobbnak kell lennie mint 0");
            });
        }
    }
}
