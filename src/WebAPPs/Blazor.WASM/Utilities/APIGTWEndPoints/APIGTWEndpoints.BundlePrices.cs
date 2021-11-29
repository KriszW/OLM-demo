using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class BundlePrices
        {
            public static string GetPaginationList()
                => BuildBaseURL() + "bp/m/list";

            public static string GetByID(int id)
                => BuildBaseURL() + $"bp/m/{id}";

            public static string Modify(int id)
                => BuildBaseURL() + $"bp/m/modify/{id}";

            public static string Delete(int id)
                => BuildBaseURL() + $"bp/m/delete/{id}";

            public static string Upload()
                => BuildBaseURL() + $"bp/m/upload";

            public static class Uploader
            {
                public static string BuildUpload()
                    => BuildBaseURL() + "bundles/prices/files/upload/powerbi";
            }
        }
    }
}
