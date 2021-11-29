using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OLM.Services.TCO.API.Tests.TestExtensions
{
    public static class EnumarableExtensions
    {
        public static string ConvertDataIntoOneString(this IEnumerable<string> source)
        {
            var stringBuilder = new StringBuilder();
            var bundleIDsForConString = source.Select(b => b + "-");

            foreach (var item in bundleIDsForConString)
            {
                stringBuilder.Append(item);
            }

            return stringBuilder.ToString();
        }
    }
}
