using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class Machine
        {
            public static string GetMachineData(string machineName)
                => BuildBaseURL() + $"bundle/machine/{machineName}";

            public static string GetMachinesData()
                => BuildBaseURL() + $"bundles/machines";
        }
    }
}
