using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Services.SharedBases.APIErrors
{
    public class APIErrorItem
    {
        public APIErrorItem() : this(string.Empty, string.Empty) { }

        public APIErrorItem(string msg) : this(string.Empty, msg) { }

        [JsonConstructor]
        public APIErrorItem(string fieldName, string errorMSG)
        {
            FieldName = fieldName;
            ErrorMSG = errorMSG;
        }

        public string FieldName { get; set; }

        public string ErrorMSG { get; set; }
    }
}
