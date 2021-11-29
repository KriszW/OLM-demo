using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.OneMachine;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.SummarizedMachines;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Extensions.OneOfExtensions;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations
{
    public class BundleMachineAggregator : IBundleMachineAggregator
    {
        private readonly ILatestBundleWithBundlePriceForMachineAggregator _latestBundleWithBundlePriceForMachineAggregator;
        private readonly IDailyBundlesWithBundlePriceForMachineAggregator _dailyBundlesWithBundlePriceForMachineAggregator;
        private readonly IWeeklyBundlesWithBundleIDForMachineAggregator _weeklyBundlesWithBundleIDForMachineAggregator;
        private readonly ISummarizedDailyBundlesWithBundlePriceForMachinesAggreagator _summarizedDailyBundlesWithBundlePriceForMachinesAggreagator;
        private readonly ISummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator _summarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator;

        public BundleMachineAggregator(ILatestBundleWithBundlePriceForMachineAggregator latestBundleWithBundlePriceForMachineAggregator,
                                       IDailyBundlesWithBundlePriceForMachineAggregator dailyBundlesWithBundlePriceForMachineAggregator,
                                       IWeeklyBundlesWithBundleIDForMachineAggregator weeklyBundlesWithBundleIDForMachineAggregator,
                                       ISummarizedDailyBundlesWithBundlePriceForMachinesAggreagator summarizedDailyBundlesWithBundlePriceForMachinesAggreagator,
                                       ISummarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator summarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator)
        {
            _latestBundleWithBundlePriceForMachineAggregator = latestBundleWithBundlePriceForMachineAggregator;
            _dailyBundlesWithBundlePriceForMachineAggregator = dailyBundlesWithBundlePriceForMachineAggregator;
            _weeklyBundlesWithBundleIDForMachineAggregator = weeklyBundlesWithBundleIDForMachineAggregator;
            _summarizedDailyBundlesWithBundlePriceForMachinesAggreagator = summarizedDailyBundlesWithBundlePriceForMachinesAggreagator;
            _summarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator = summarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator;
        }

        public async Task<APIResponse<MachineViewModel>> GetDataForMachine(string machineID)
        {
            var output = new APIResponse<MachineViewModel>() { Model = new MachineViewModel(), Success = true };

            var latestTask = _latestBundleWithBundlePriceForMachineAggregator.FetchLatestBundle(machineID);
            var dailyTask = _dailyBundlesWithBundlePriceForMachineAggregator.FetchDailyBundles(machineID);
            var weeklyTask = _weeklyBundlesWithBundleIDForMachineAggregator.FetchWeeklyBundles(machineID);

            await Task.WhenAll(latestTask, dailyTask, weeklyTask);

            output.Errors = new APIError();

            if (latestTask.Result.MatchError())
            {
                foreach (var item in latestTask.Result.AsT1.Errors)
                {
                    output.Errors.Add(new APIErrorItem("latest", item.ErrorMSG));
                }
            }
            else
            {
                output.Model.Latest = latestTask.Result.AsT0;
            }

            if (dailyTask.Result.MatchError())
            {
                foreach (var item in dailyTask.Result.AsT1.Errors)
                {
                    output.Errors.Add(new APIErrorItem("daily", item.ErrorMSG));
                }
            }
            else
            {
                output.Model.Daily = dailyTask.Result.AsT0;
            }

            if (weeklyTask.Result.MatchError())
            {
                foreach (var item in weeklyTask.Result.AsT1.Errors)
                {
                    output.Errors.Add(new APIErrorItem("weekly", item.ErrorMSG));
                }
            }
            else
            {
                output.Model.Weekly = weeklyTask.Result.AsT0;
            }


            return output;
        }

        public async Task<APIResponse<SummarizedMachineViewModel>> GetDataForMachines()
        {
            var output = new APIResponse<SummarizedMachineViewModel>() { Model = new SummarizedMachineViewModel(), Success = true };

            var dailyTask = _summarizedDailyBundlesWithBundlePriceForMachinesAggreagator.FetchSummarizedDaily();
            var weeklyTask = _summarizedWeeklyBundlesWithBundlePriceForMachinesAggreagator.FetchSummarizedWeekly();

            await Task.WhenAll(dailyTask, weeklyTask);

            output.Errors = new APIError();

            if (dailyTask.Result.MatchError())
            {
                foreach (var item in dailyTask.Result.AsT1.Errors)
                {
                    output.Errors.Add(new APIErrorItem("daily", item.ErrorMSG));
                }
            }
            else
            {
                output.Model.Daily = dailyTask.Result.AsT0;
            }

            if (weeklyTask.Result.MatchError())
            {
                foreach (var item in weeklyTask.Result.AsT1.Errors)
                {
                    output.Errors.Add(new APIErrorItem("weekly", item.ErrorMSG));
                }
            }
            else
            {
                output.Model.Weekly = weeklyTask.Result.AsT0;
            }

            return output;
        }
    }
}
