using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class Routing
        {
            public static string Calculate(string machineID)
                => BuildBaseURL() + $"routing/{machineID}";

            public static string CalculateForDay(string machineID)
                => BuildBaseURL() + $"routing/daily/{machineID}";

            public static string CalculateForWeek(string machineID)
                => BuildBaseURL() + $"routing/weekly/{machineID}";

            public static class Manager
            {
                public static string GetPaginationList()
                    => BuildBaseURL() + "r/m/list";

                public static string GetByID(int id)
                    => BuildBaseURL() + $"r/m/{id}";

                public static string Modify(int id)
                    => BuildBaseURL() + $"r/m/modify/{id}";

                public static string Delete(int id)
                    => BuildBaseURL() + $"r/m/delete/{id}";

                public static string Upload()
                    => BuildBaseURL() + $"r/m/upload";
            }
        }
    }
}
