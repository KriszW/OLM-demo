using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Routing
{
    public class DummyRoutingRepository : IRoutingRepository
    {
        public Task<APIResponse<AggregatedRoutingViewModel>> Fetch(string machineID)
            => Task.FromResult(new APIResponse<AggregatedRoutingViewModel> 
            {
                Success = true,
                Model = new AggregatedRoutingViewModel
                {
                    Daily = new RoutingResponseViewModel
                    {
                        Data = new List<RoutingsDataResponseViewModel>
                        {
                            new RoutingsDataResponseViewModel
                            {
                                Dimension = "25 x 75",
                                ActualRouting = 51303,
                                ExpectedRouting = 50230,
                            },
                            new RoutingsDataResponseViewModel
                            {
                                Dimension = "25 x 100",
                                ActualRouting = 14321,
                                ExpectedRouting = 14230,
                            },
                            new RoutingsDataResponseViewModel
                            {
                                Dimension = "19 x 75",
                                ActualRouting = 5003,
                                ExpectedRouting = 5230,
                            },
                        },
                    },
                    Weekly = new RoutingResponseViewModel
                    {
                        Data = new List<RoutingsDataResponseViewModel>
                        {
                            new RoutingsDataResponseViewModel
                            {
                                Dimension = "25 x 75",
                                ActualRouting = 156702,
                                ExpectedRouting = 145023,
                            },
                            new RoutingsDataResponseViewModel
                            {
                                Dimension = "25 x 100",
                                ActualRouting = 55421,
                                ExpectedRouting = 50431,
                            },
                            new RoutingsDataResponseViewModel
                            {
                                Dimension = "19 x 75",
                                ActualRouting = 15430,
                                ExpectedRouting = 15430,
                            },
                            new RoutingsDataResponseViewModel
                            {
                                Dimension = "19 x 125",
                                ActualRouting = 17000,
                                ExpectedRouting = 14534,
                            },
                        },
                    }
                }
            });

        public Task<APIResponse<RoutingResponseViewModel>> FetchForDay(string machineID)
            => Task.FromResult(new APIResponse<RoutingResponseViewModel>
            {
                Success = true,
                Model = new RoutingResponseViewModel
                {
                    Data = new List<RoutingsDataResponseViewModel>
                    {
                        new RoutingsDataResponseViewModel
                        {
                            Dimension = "25 x 75",
                            ActualRouting = 74310,
                            ExpectedRouting = 67541,
                        },
                        new RoutingsDataResponseViewModel
                        {
                            Dimension = "25 x 100",
                            ActualRouting = 31266,
                            ExpectedRouting = 34512,
                        },
                        new RoutingsDataResponseViewModel
                        {
                            Dimension = "19 x 75",
                            ActualRouting = 6783,
                            ExpectedRouting = 6783,
                        },
                    },
                }
            });

        public Task<APIResponse<RoutingResponseViewModel>> FetchForWeek(string machineID)
            => Task.FromResult(new APIResponse<RoutingResponseViewModel>
            {
                Success = true,
                Model = new RoutingResponseViewModel
                {
                    Data = new List<RoutingsDataResponseViewModel>
                    {
                        new RoutingsDataResponseViewModel
                        {
                            Dimension = "25 x 75",
                            ActualRouting = 174250,
                            ExpectedRouting = 167000,
                        },
                        new RoutingsDataResponseViewModel
                        {
                            Dimension = "25 x 100",
                            ActualRouting = 45312,
                            ExpectedRouting = 45312,
                        },
                        new RoutingsDataResponseViewModel
                        {
                            Dimension = "19 x 75",
                            ActualRouting = 247220,
                            ExpectedRouting = 254023,
                        },
                        new RoutingsDataResponseViewModel
                        {
                            Dimension = "19 x 100",
                            ActualRouting = 235150,
                            ExpectedRouting = 220235,
                        },
                    },
                }
            });
    }
}
