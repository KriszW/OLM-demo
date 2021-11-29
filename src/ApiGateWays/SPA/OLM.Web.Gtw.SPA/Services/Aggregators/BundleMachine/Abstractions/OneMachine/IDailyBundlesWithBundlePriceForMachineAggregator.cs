using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OneOf;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.OneMachine
{
    public interface IDailyBundlesWithBundlePriceForMachineAggregator
    {
        Task<OneOf<DailyMachineDataViewModel, APIError>> FetchDailyBundles(string machineID);
    }
}
