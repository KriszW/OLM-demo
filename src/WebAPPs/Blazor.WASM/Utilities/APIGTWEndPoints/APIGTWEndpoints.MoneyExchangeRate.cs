using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class CurrencyManager
        {
            public static string All()
                => BuildBaseURL() + "mer/cur/m/all";

            public static string GetPaginationList()
                => BuildBaseURL() + "mer/cur/m/list";

            public static string GetByID(int id)
                => BuildBaseURL() + $"mer/cur/m/{id}";

            public static string Modify(int id)
                => BuildBaseURL() + $"mer/cur/m/modify/{id}";

            public static string Delete(int id)
                => BuildBaseURL() + $"mer/cur/m/delete/{id}";

            public static string Upload()
                => BuildBaseURL() + $"mer/cur/m/upload";
        }

        public static class ExchangeRateManager
        {
            public static string GetPaginationList()
                => BuildBaseURL() + "mer/er/m/list";

            public static string GetByID(int id)
                => BuildBaseURL() + $"mer/er/m/{id}";

            public static string Modify(string isoCode)
                => BuildBaseURL() + $"mer/er/m/modify/{isoCode}";

            public static string Delete(int id)
                => BuildBaseURL() + $"mer/er/m/delete/{id}";

            public static string Upload()
                => BuildBaseURL() + $"mer/er/m/upload";
        }
    }
}
