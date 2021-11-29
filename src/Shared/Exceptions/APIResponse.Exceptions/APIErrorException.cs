using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Shared.Exceptions.APIResponse.Exceptions
{


    public class APIErrorException : Exception
    {
        public APIErrorException(string errorMessage) : this(new APIError(errorMessage)) { }

        [JsonConstructor]
        public APIErrorException(APIError errors) : base(errors.TryGetMessage())
        {
            APIErrors = errors;
        }

        public APIError APIErrors { get; private set; }
    }
}
