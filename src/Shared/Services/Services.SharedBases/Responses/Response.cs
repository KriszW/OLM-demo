using OLM.Services.SharedBases.Abstractions.Response;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Services.SharedBases.Responses
{
    public class APIResponse<TModel> : APIResponseBase, IResponse<TModel>
    {
        public APIResponse() { }
        /// <summary>
        /// Success will be false, and the Model will be default
        /// </summary>
        /// <param name="errors"></param>
        public APIResponse(APIError errors) : this(false, errors, Guid.NewGuid(), default) { }

        public APIResponse(string msg) : this(false, new APIError(msg), Guid.NewGuid(), default) { }
        /// <summary>
        /// Success will be true, and errors will be empty
        /// </summary>
        public APIResponse(TModel model)
        {
            Model = model;
        }

        /// <summary>
        /// Success will be false
        /// </summary>
        public APIResponse(APIError errors, TModel model) : base(errors)
        {
            Model = model;
        }

        public APIResponse(APIError errors, TModel model, bool success = false) : base(errors, success)
        {
            Model = model;
        }


        [JsonConstructor]
        public APIResponse(bool success, APIError errors, Guid iD, TModel model) : base(success, errors, iD)
        {
            Model = model;
        }

        public TModel Model { get; set; }

        /// <summary>
        /// If there is an error in the response it throws an <see cref="APIErrorException"/> exception
        /// If not then returns the Model of the APIResponse
        /// </summary>
        /// <exception cref="APIErrorException"></exception>
        public TModel TryGetModel()
        {
            if (Success) return Model;
            else throw new APIErrorException(Errors);
        }
    }
}
