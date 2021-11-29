using OLM.Shared.Exceptions.APIResponse.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Extensions
{
    public static class TaskExceptionExtensions
    {
        public static TModel TryGetResult<TModel>(this Task<TModel> task)
        {
            if (task.IsFaulted)
            {
                var ex = task.Exception.InnerExceptions.FirstOrDefault();

                if (ex != default && ex is APIErrorException apiErrorException)
                {
                    throw apiErrorException;
                }
            }

            return task.Result;
        }
    }
}
