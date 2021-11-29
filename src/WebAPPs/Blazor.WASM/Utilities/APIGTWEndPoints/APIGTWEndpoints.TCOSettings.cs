using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class TCOSettings
        {
            public static string GetPaginationList()
                => BuildBaseURL() + "t/s/v/list";

            public static string GetByID(int id)
                => BuildBaseURL() + $"t/s/v/{id}";

            public static string Modify(int id)
                => BuildBaseURL() + $"t/s/v/modify/{id}";

            public static string Delete(int id)
                => BuildBaseURL() + $"t/s/v/delete/{id}";

            public static string Upload()
                => BuildBaseURL() + $"t/s/v/upload";
        }
    }
}
