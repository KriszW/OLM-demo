using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class CategoryBulbs
        {
            public static class ItemNumberManager
            {
                public static string Search()
                    => BuildBaseURL() + "cb/c/i/search";

                public static string GetPaginationList()
                    => BuildBaseURL() + "cb/c/i/list";

                public static string GetByID(int id)
                    => BuildBaseURL() + $"cb/c/i/{id}";

                public static string Modify(int id)
                    => BuildBaseURL() + $"cb/c/i/modify/{id}";

                public static string Delete(int id)
                    => BuildBaseURL() + $"cb/c/i/delete/{id}";

                public static string Upload()
                    => BuildBaseURL() + $"cb/c/i/upload";
            }
        }
    }
}
