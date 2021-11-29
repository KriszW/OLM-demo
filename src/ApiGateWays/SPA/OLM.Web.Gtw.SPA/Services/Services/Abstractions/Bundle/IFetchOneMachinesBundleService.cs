using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.Bundles.APIResponses;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle
{
    public interface IFetchOneMachinesBundleService
    {
        Task<OneOf<BundleAPIResponseViewModel, APIError>> FetchLatestBundle(string machineName);
        Task<OneOf<SummarizedBundlesForMachineDataViewModel, APIError>> FetchDailyBundle(string machineName);
        Task<OneOf<SummarizedBundlesForMachineDataViewModel, APIError>> FetchWeeklyBundle(string machineName);
    }
}
