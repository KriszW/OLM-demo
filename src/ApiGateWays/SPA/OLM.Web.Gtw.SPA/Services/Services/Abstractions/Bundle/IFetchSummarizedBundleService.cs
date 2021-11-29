using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle
{
    public interface IFetchSummarizedBundleService
    {
        Task<OneOf<SummarizedDataForMachinesViewModel, APIError>> FetchDaily();
        Task<OneOf<SummarizedDataForMachinesViewModel, APIError>> FetchWeekly();
    }
}
