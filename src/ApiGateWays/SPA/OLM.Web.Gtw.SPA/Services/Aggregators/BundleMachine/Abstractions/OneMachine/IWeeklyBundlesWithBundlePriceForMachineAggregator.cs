using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.OneMachine
{
    public interface IWeeklyBundlesWithBundleIDForMachineAggregator
    {
        Task<OneOf<WeeklyMachineDataViewModel, APIError>> FetchWeeklyBundles(string machineID);
    }
}
