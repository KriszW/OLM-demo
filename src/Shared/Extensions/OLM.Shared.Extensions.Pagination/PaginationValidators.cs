using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Extensions.Pagination
{
    public static class PaginationValidators
    {
        public static IRuleBuilderOptions<int, int> ValidatePaginationPageIndex(this IRuleBuilder<int, int> ruleBuilder) 
            => ruleBuilder.GreaterThanOrEqualTo(0).WithMessage("A pageIndex paraméter nem lehet kisebb mint 0");

        public static IRuleBuilderOptions<int, int> ValidatePaginationPageSize(this IRuleBuilder<int, int> ruleBuilder)
            => ruleBuilder.GreaterThan(0).WithMessage("A pageSize paraméternek nagyobbnak kell lennie mint 0");
    }
}
