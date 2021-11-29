using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Services.SharedBases.Responses
{
    public class EmptyAPIResponse : APIResponseBase
    {
        public EmptyAPIResponse() : this(true,default, Guid.NewGuid()) { }

        public EmptyAPIResponse(string msg) : this(false, new APIError(msg), Guid.NewGuid()) { }

        public EmptyAPIResponse(APIError errors) : base(errors) { }

        public EmptyAPIResponse(APIError errors, bool success = false) : base(errors, success) { }

        [JsonConstructor]
        public EmptyAPIResponse(bool success, APIError errors, Guid iD) : base(success, errors, iD) { }
    }
}
