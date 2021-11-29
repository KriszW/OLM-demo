using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Extensions.APIErrorExceptionHandler
{
    public class APIErrorExceptionHandlerFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is APIErrorException apiErrorException)
            {
                //var result = new EmptyAPIResponse
                //{
                //    Errors = context.Exception switch
                //    {
                //        APIErrorException ex => ex.APIErrors,
                //        AggregateException ex => new APIError(ex.InnerExceptions.Select(e => new APIErrorItem("", e.ToString()))),
                //        Exception ex => new APIError(ex.ToString()),
                //    }
                //};

                context.Result = new BadRequestObjectResult(new EmptyAPIResponse(apiErrorException.APIErrors));
        }
            

            

            return Task.CompletedTask;
        }
    }
}
