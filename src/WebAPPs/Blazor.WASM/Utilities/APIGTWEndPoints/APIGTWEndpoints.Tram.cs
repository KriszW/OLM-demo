using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class Tram
        {
            public static class DimensionManager
            {
                public static string GetAll()
                    => BuildBaseURL() + "tram/dimension/all";

                public static string GetPaginationList()
                    => BuildBaseURL() + "tram/td/m/list";

                public static string GetByID(int id)
                    => BuildBaseURL() + $"tram/td/m/{id}";

                public static string Modify(int id)
                    => BuildBaseURL() + $"tram/td/m/modify/{id}";

                public static string Delete(int id)
                    => BuildBaseURL() + $"tram/td/m/delete/{id}";

                public static string Upload()
                    => BuildBaseURL() + $"tram/td/m/upload";
            }

            public static class DataUploader
            {
                public static string Upload()
                    => BuildBaseURL() + $"tram/data/upload/";
            }

            public static class DataManager
            {
                public static string GetPaginationList()
                    => BuildBaseURL() + "tram/d/m/list";

                public static string GetByID(int id)
                    => BuildBaseURL() + $"tram/d/m/{id}";

                public static string Modify(int id)
                    => BuildBaseURL() + $"tram/d/m/modify/{id}";

                public static string Delete(int id)
                    => BuildBaseURL() + $"tram/d/m/delete/{id}";

                public static string Upload()
                    => BuildBaseURL() + $"tram/d/m/upload";
            }
        }
    }
}
