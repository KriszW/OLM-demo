using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid == false)
            {
                var errorsInModelState = context.ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());

                var errorMSGs = GetModels(errorsInModelState);

                var result = new EmptyAPIResponse(new APIError(errorMSGs));
                context.Result = new BadRequestObjectResult(result);
                return;
            }

            await next();
        }

        private static List<APIErrorItem> GetModels(Dictionary<string, string[]> errorsInModelState)
        {
            var errorMSGs = new List<APIErrorItem>();

            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {
                    var newModel = new APIErrorItem()
                    {
                        FieldName = error.Key,
                        ErrorMSG = subError
                    };

                    errorMSGs.Add(newModel);
                }
            }

            return errorMSGs;
        }
    }
}
