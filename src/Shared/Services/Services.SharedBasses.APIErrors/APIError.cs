using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Services.SharedBases.APIErrors
{
    public class APIError
    {
        private readonly List<APIErrorItem> _items;

        public APIError() : this(default(IEnumerable<APIErrorItem>)) { }

        public APIError(string msg) : this(new APIErrorItem[] { new APIErrorItem(msg) }) { }

        public APIError(string fieldName, string msg) : this(new APIErrorItem[] { new APIErrorItem(fieldName, msg) }) { }

        [JsonConstructor]
        public APIError(IEnumerable<APIErrorItem> errors)
        {
            _items = errors?.ToList() ?? new List<APIErrorItem>();
        }

        public IEnumerable<APIErrorItem> Errors => _items;

        public void Add(APIErrorItem error)
        {
            if (error != default) _items.Add(error);
        }

        public void AddRange(IEnumerable<APIErrorItem> errors)
        {
            if (errors != default) _items.AddRange(errors);
        }

        public string TryGetMessage() => _items?.FirstOrDefault()?.ErrorMSG;

        public int ErrorCount() => (_items?.Count()).GetValueOrDefault(0);

        public bool AnyError() => (_items?.Any()).GetValueOrDefault(false);

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }
    }
}
