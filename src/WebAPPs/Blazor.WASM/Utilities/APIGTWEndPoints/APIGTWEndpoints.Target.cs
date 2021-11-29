using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class WasteTarget
        {
            public static class Manager
            {
                public static string GetPaginationList()
                    => BuildBaseURL() + "target/wt/m/list";

                public static string GetByID(int id)
                    => BuildBaseURL() + $"target/wt/m/{id}";

                public static string Modify(int id)
                    => BuildBaseURL() + $"target/wt/m/modify/{id}";

                public static string Delete(int id)
                    => BuildBaseURL() + $"target/wt/m/delete/{id}";

                public static string Upload()
                    => BuildBaseURL() + $"target/wt/m/upload";
            }
        }
    }
}
