using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions.SummarizedMachines;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.BundlePrices;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Extensions.OneOfExtensions;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Implementations.SummarizedMachines
{
    public class SummarizedDailyBundlesWithBundlePriceForMachinesAggreagator : ISummarizedDailyBundlesWithBundlePriceForMachinesAggreagator
    {
        private readonly IFetchSummarizedBundleService _fetchSummarizedBundleService;
        private readonly IFetchRawTCOCalculatorService _tcoCalculatorService;
        private readonly IFetchBundlePriceService _fetchBundlePriceService;

        public SummarizedDailyBundlesWithBundlePriceForMachinesAggreagator(IFetchSummarizedBundleService fetchSummarizedBundleService,
                                                                           IFetchRawTCOCalculatorService tcoCalculatorService,
                                                                           IFetchBundlePriceService fetchBundlePriceService)
        {
            _fetchSummarizedBundleService = fetchSummarizedBundleService;
            _tcoCalculatorService = tcoCalculatorService;
            _fetchBundlePriceService = fetchBundlePriceService;
        }

        public async Task<OneOf<DailySummarizedViewModel, APIError>> FetchSummarizedDaily()
        {
            var oneOfBundle = await _fetchSummarizedBundleService.FetchDaily();

            if (oneOfBundle.MatchError()) return oneOfBundle.AsT1;

            var result = oneOfBundle.AsT0;

            var oneOfBundlePrice = await _fetchBundlePriceService.Fetch(result.BundleIDs);

            if (oneOfBundlePrice.MatchError()) return oneOfBundlePrice.AsT1;
            var bundlePrices = oneOfBundlePrice.AsT0;

            var oneOfTCOresult = await _tcoCalculatorService.CalculateAVGTCO(bundlePrices);

            if (oneOfTCOresult.MatchError()) return oneOfTCOresult.AsT1;
            var tcoresult = oneOfTCOresult.AsT0;

            return new DailySummarizedViewModel()
            {
                AllFS = result.AllFS,
                AllGoodProduced = result.AllProduced,
                AllInput = result.AllInput,
                WastePercentage = result.AVGWastePercentage,
                TCO = new TCODataViewModel(tcoresult.ExpectedTCOValue,
                       tcoresult.CalculatedValue,
                       tcoresult.MaximumDifference)
            };
        }
    }
}
