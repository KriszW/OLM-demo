using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class Bundles
        {
            public static string GetPaginationList()
                => BuildBaseURL() + "b/bundle/tco/list";
        }
    }
}