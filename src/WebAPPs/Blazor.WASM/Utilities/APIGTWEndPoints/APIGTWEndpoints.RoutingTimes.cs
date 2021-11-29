using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class RoutingTimes
        {
            public static class PauseManager
            {
                public static string GetPaginationListOfWeeks()
                    => BuildBaseURL() + "rt/p/m/list/weeks";

                public static string GetPaginationList()
                    => BuildBaseURL() + "rt/p/m/list/data";

                public static string GetByID(int id)
                    => BuildBaseURL() + $"rt/p/m/{id}";

                public static string Modify(int id)
                    => BuildBaseURL() + $"rt/p/m/modify/{id}";

                public static string Delete(int id)
                    => BuildBaseURL() + $"rt/p/m/delete/{id}";

                public static string Upload()
                    => BuildBaseURL() + $"rt/p/m/upload";
            }

            public static class ProdTimeManager
            {
                public static string GetPaginationListOfWeeks()
                    => BuildBaseURL() + "rt/pt/m/list/weeks";

                public static string GetPaginationList()
                    => BuildBaseURL() + "rt/pt/m/list/data";

                public static string GetByID(int id)
                    => BuildBaseURL() + $"rt/pt/m/{id}";

                public static string Modify(int id)
                    => BuildBaseURL() + $"rt/pt/m/modify/{id}";

                public static string Delete(int id)
                    => BuildBaseURL() + $"rt/pt/m/delete/{id}";

                public static string Upload()
                    => BuildBaseURL() + $"rt/pt/m/upload";
            }
        }
    }
}
