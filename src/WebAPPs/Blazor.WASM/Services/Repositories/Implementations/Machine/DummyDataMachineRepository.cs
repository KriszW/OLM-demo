using OLM.Blazor.WASM.Services.Repositories.Abstractions.Machine;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using OLM.Shared.Models.Bundles.APIResponses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Shared.Models.CategoryBulbs.APIResponses;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Machine
{
    public class DummyDataMachineRepository : IMachineRepository
    {
        public Task<APIResponse<MachineViewModel>> FetchMachineData(string machineName)
            => Task.FromResult(
                new APIResponse<MachineViewModel> 
                {
                    Success = true,
                    Errors = default,
                    ID = Guid.NewGuid(),
                    Model = new MachineViewModel
                    {
                        Latest = new LatestBundleViewModel
                        {
                            Bundle = new BundleAPIResponseViewModel
                            {
                                BundleID = "test",
                                FS = 432,
                                Input = 2540,
                                Produced = 2150,
                                MachineName = machineName,
                                WastePercentage = 0.234,
                                Dimension = "19 * 125",
                                Quality = "VI",
                                VendorName = "UPM",
                                SawmillName = "UPM Kalkova",
                            },
                            CategoryBulbs = new List<ValidationResult>
                            {
                                new ValidationResult() { ValidationSucceded = false},
                                new ValidationResult() { ValidationSucceded = true},
                                new ValidationResult() { ValidationSucceded = false},
                                new ValidationResult() { ValidationSucceded = true}
                            },
                            TCO = new TCODataViewModel(353,345, 0.2),
                        },
                        Daily = new DailyMachineDataViewModel
                        {
                            AllInput = 45670,
                            AllFS = 5325,
                            AllGoodProduced = 38215,
                            WastePercentage = 0.23,
                            TCO = new TCODataViewModel(256, 254, 0.5)
                        },
                        Weekly = new WeeklyMachineDataViewModel
                        {
                            AllInput = 195603,
                            AllFS = 15325,
                            AllGoodProduced = 180215,
                            WastePercentage = 0.2034,
                            TCO = new TCODataViewModel(401, 223, 0.3)
                        }
                    }
                }
            );

        public Task<APIResponse<SummarizedMachineViewModel>> FetchSummarizedData()
            => Task.FromResult(
                new APIResponse<SummarizedMachineViewModel>
                {
                    Success = true,
                    Errors = default,
                    ID = Guid.NewGuid(),
                    Model = new SummarizedMachineViewModel
                    {
                        Daily = new DailySummarizedViewModel
                        {
                            AllInput = 160670,
                            AllFS = 25025,
                            AllGoodProduced = 140215,
                            WastePercentage = 0.13,
                            TCO = new TCODataViewModel(326, 345, 2.0)
                        },
                        Weekly = new WeeklySummarizedViewModel
                        {
                            AllInput = 753210,
                            AllFS = 154210,
                            AllGoodProduced = 661357,
                            WastePercentage = 0.25,
                            TCO = new TCODataViewModel(371, 367, 1.6)
                        }
                    }
                }
            );
    }
}
