using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        public static class Bundles
        {
            //public const string APIVersion = "v1/";
            public const string APIVersion = "";

            public static class Bundle
            {
                public static string GetBundles(string baseURL, DateTime from, DateTime to)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundle?from={from:s}&to={to:s}";
                public static string GetLatestBundleURL(string baseURL, string machineName)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundle/latest/{machineName}";
                public static string GetDailyBundleURL(string baseURL, string machineName)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) +  $"bundle/daily/{machineName}";
                public static string GetWeeklyBundleURL(string baseURL, string machineName)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundle/weekly/{machineName}";
            }
            public static class BundlesSumerized
            {
                public static string GetDailyBundleURL(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + "bundles/daily/";
                public static string GetWeeklyBundleURL(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + "bundles/weekly/";
            }
            public static class MachineSummarizedData
            {
                public static string GetDailyBundleURL(string baseURL, string machineName)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundles/sumdata/{machineName}/daily";
                public static string GetWeeklyBundleURL(string baseURL, string machineName)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundles/sumdata/{machineName}/weekly";
            }
            public static class MachinesSummarizedData
            {
                public static string GetDailyBundleURL(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + "bundles/sumdata/daily";
                public static string GetWeeklyBundleURL(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + "bundles/sumdata/weekly";
            }
        }
    }
}
