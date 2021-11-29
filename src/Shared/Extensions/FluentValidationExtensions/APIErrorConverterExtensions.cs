using FluentValidation.Results;
using OLM.Services.SharedBases.APIErrors;
using System.Linq;

namespace DocShow.Shared.Extensions.FluentValidationExtensions
{
    public static class APIErrorConverterExtensions
    {
        public static APIError ToAPIError(this ValidationResult validationResult) =>
            new(validationResult.Errors.Select(m => new APIErrorItem(m.ErrorCode, m.ErrorMessage)));
    }
}
