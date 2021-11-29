using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.SummarizedMachines
{
    public interface ISummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator
    {
        Task<OneOf<WeeklySummarizedViewModel, APIError>> FetchSummarizedWeekly();
    }
}
