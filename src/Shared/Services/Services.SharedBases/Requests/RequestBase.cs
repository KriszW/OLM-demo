using OLM.Services.SharedBases.Abstractions.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Services.SharedBases.Requests
{
    public class APIRequestBase : IRequestBase
    {
        public APIRequestBase() : this(DateTime.Now, Guid.NewGuid()) { }

        [JsonConstructor]
        public APIRequestBase(DateTime creationDate, Guid iD)
        {
            CreationDate = creationDate;
            ID = iD;
        }

        public DateTime? CreationDate { get; set; }

        public Guid ID { get; set; }
    }
}
