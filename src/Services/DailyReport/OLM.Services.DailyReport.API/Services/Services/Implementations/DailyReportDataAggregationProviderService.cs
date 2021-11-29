using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.Services.Services.Abstractions;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations
{
    public class DailyReportDataAggregationProviderService : IDailyReportDataAggregationProviderService
    {
        public async Task<DailyReportSummarizedForDimensionViewModel> AggregateForDimension(ModelAggregatorViewModel model)
        {
            var taskAllLength = Task.Run(() => model.Data.Sum(m => m.Length));
            var taskAllWaste = Task.Run(() => model.Data.Sum(m => m.LengthOfWaste));
            var taskAllFS = Task.Run(() => model.Data.Sum(m => m.LengthOfFS));

            await Task.WhenAll(taskAllLength, taskAllWaste, taskAllFS);

            var allLengthM3 = taskAllLength.Result * model.TargetModel.Intersection;
            var allLengthOfWasteM3 = taskAllWaste.Result * model.TargetModel.Intersection;
            var allLengthOfFSM3 = taskAllFS.Result * model.TargetModel.Intersection;

            var allTram = model.TramModel.NumberOfTram * 0.4;
            var allLamella = model.TramModel.NumberOfLammela * model.TargetModel.Intersection;

            return new DailyReportSummarizedForDimensionViewModel 
            {
                TotalM3 = allLengthM3,
                WasteM3 = allLengthOfWasteM3,
                FSM3 = allLengthOfFSM3,
                TramM3 = allTram,
                LammelaM3 = allLamella,
                TotalWasteM3 = allLengthOfWasteM3 + allLengthOfFSM3 + allTram + allLamella,
                TargetWasteM3 = allLengthM3 * model.TargetModel.Target
            };
        }
    }
}
