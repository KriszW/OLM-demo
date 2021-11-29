using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using OneOf;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using OneOf.Types;
using System.Reflection;

namespace OLM.Shared.Extensions.OneOfExtensions
{
    public static class OneOfMatchExtensions
    {
        /// <summary>
        /// If there is an error in the response it returns an <see cref="APIError"/>
        /// If not then returns <see cref="Model"/>
        /// </summary>
        public static OneOf<TModel, APIError> TryMatchModel<TModel>(this APIResponse<TModel> response)
        {
            if (response == default) throw new ArgumentNullException(nameof(response));

            return (response.Errors?.AnyError()).GetValueOrDefault(false) ? response.Errors : response.Model;
        }

        public static bool MatchError<T1, T2>(this OneOf<T1, T2> oneof)
        {
            return oneof.Match(
                MatchPotentialError,
                MatchPotentialError
            );
        }

        public static bool MatchSuccess<T1, T2>(this OneOf<T1, T2> oneof)
        {
            return oneof.Match(
                MatchPotentialSuccess,
                MatchPotentialSuccess
            );
        }

        public static ActionResult MatchResponse<T1, T2>(this OneOf<T1, T2> oneof)
        {
            return oneof.Match(
                MatchPotentialAPIError,
                MatchPotentialAPIError
            );
        }

        private static bool MatchPotentialError<T>(T t) => t is APIError;

        private static bool MatchPotentialSuccess<T>(T t) => t is not APIError;

        private static ActionResult MatchPotentialAPIError<T>(T t)
        {
            if (t is APIError errors)
            {
                return new BadRequestObjectResult(new APIResponse<T>(errors));
            }

            return new OkObjectResult(new APIResponse<T>(t));
        }
    }
}
