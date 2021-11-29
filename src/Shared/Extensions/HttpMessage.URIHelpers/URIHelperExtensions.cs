using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OLM.Shared.Extensions.HttpMessage.URIHelpers
{
    public static class URIHelperExtensions
    {
        public static string ToOneLineQueryString(this IEnumerable<string> data, string keyName)
        {
            var output = "";

            if (data.Any() == false) return output;

            foreach (var item in data)
            {
                output += $"{keyName}={item}&";
            }

            return output.Remove(output.Length-1);
        }

        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }

        public static string ToQueryString(this IEnumerable<object> request, string keyName)
        {
            var output = "";

            if (request.Any() == false) return output;

            foreach (var item in request)
            {
                var itemQueryString = item.ToQueryString();
                output += $"{keyName}={itemQueryString}&";
            }

            return output.Remove(output.Length - 1);
        }
    }
}
