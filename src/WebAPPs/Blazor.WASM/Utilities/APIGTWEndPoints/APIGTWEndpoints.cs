using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public const string APIVersion = "";
            
        public static string BuildBaseURL()
            => "api/" + APIVersion;
    }
}