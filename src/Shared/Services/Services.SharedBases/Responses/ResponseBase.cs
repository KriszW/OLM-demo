using OLM.Services.SharedBases.Abstractions.Response;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Services.SharedBases.Responses
{
    public class APIResponseBase : IResponseBase
    {
        public APIResponseBase() : this(true, default, Guid.NewGuid()) { }
        public APIResponseBase(APIError errors) : this(false, errors, Guid.NewGuid()) { }
        public APIResponseBase(APIError errors, bool success = false) : this(success, errors, Guid.NewGuid()) { }

        [JsonConstructor]
        public APIResponseBase(bool success, APIError errors, Guid iD)
        {
            Success = success;
            ID = iD;
            Errors = errors;
        }

        public bool Success { get; set; }
        public APIError Errors { get; set; }
        public Guid ID { get; set; }

        /// <summary>
        /// It is supported because of legacy support
        /// Version 2.0 will not support Message as error reporting
        /// Use instead <see cref="Errors"/> property
        /// </summary>
        public string Message
        {
            get
            {
                return Errors?.Errors?.FirstOrDefault()?.ErrorMSG;
            }
        }
    }
}
