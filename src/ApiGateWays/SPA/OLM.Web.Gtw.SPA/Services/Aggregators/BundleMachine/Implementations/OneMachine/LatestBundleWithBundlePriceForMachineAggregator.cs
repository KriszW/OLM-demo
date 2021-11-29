using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.OneMachine;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.BundlePrices;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.CategoryBulbs;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Extensions.OneOfExtensions;
using OLM.Shared.Models.Bundles.APIResponses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations.OneMachine
{
    public class LatestBundleWithBundlePriceForMachineAggregator : ILatestBundleWithBundlePriceForMachineAggregator
    {
        private readonly IFetchOneMachinesBundleService _fetchOneMachinesBundleService;
        private readonly IFetchRawTCOCalculatorService _tcoCalculatorService;
        private readonly IFetchBundlePriceService _fetchBundlePriceService;
        private readonly IValidateOneBundleService _validateOneBundleService;

        public LatestBundleWithBundlePriceForMachineAggregator(IFetchOneMachinesBundleService fetchOneMachinesBundleService,
                                                               IFetchRawTCOCalculatorService tcoCalculatorService,
                                                               IFetchBundlePriceService fetchBundlePriceService,
                                                               IValidateOneBundleService validateOneBundleService)
        {
            _fetchOneMachinesBundleService = fetchOneMachinesBundleService;
            _tcoCalculatorService = tcoCalculatorService;
            _fetchBundlePriceService = fetchBundlePriceService;
            _validateOneBundleService = validateOneBundleService;
        }

        public async Task<OneOf<LatestBundleViewModel, APIError>> FetchLatestBundle(string machineID)
        {
            var resultMatch = await _fetchOneMachinesBundleService.FetchLatestBundle(machineID);

            if (resultMatch.MatchError()) return resultMatch.AsT1;
            var result = resultMatch.AsT0;

            var bundlePrice = await _fetchBundlePriceService.Fetch(result.BundleID);

            if (bundlePrice.MatchError()) return bundlePrice.AsT1;

            var tcoFetchTask = _tcoCalculatorService.CalculateTCO(bundlePrice.AsT0);
            var validationFetchTask = _validateOneBundleService.ValidateBundle(result.BundleID);

            await Task.WhenAll(tcoFetchTask, validationFetchTask);

            if (tcoFetchTask.Result.MatchError()) return tcoFetchTask.Result.AsT1;

            if (validationFetchTask.Result.MatchError()) return validationFetchTask.Result.AsT1;

            var tcoResult = tcoFetchTask.Result.AsT0;
            var validationResult = validationFetchTask.Result.AsT0;

            return new LatestBundleViewModel()
            {
                Bundle = new BundleAPIResponseViewModel(result.BundleID,
                                                        result.Input,
                                                        result.Produced,
                                                        result.FS,
                                                        result.WastePercentage,
                                                        result.Dimension,
                                                        result.Quality,
                                                        result.VendorName,
                                                        result.SawmillName,
                                                        machineID,
                                                        result.FinishedDate),

                TCO = new TCODataViewModel(tcoResult.ExpectedTCOValue,
                                                       tcoResult.CalculatedValue,
                                                       tcoResult.MaximumDifference),
                CategoryBulbs = validationResult,
            };
        }
    }
}
