using OLM.Services.Routing.API.Extensions;
using OLM.Services.Routing.API.Services.Repositories.Abstractions;
using OLM.Services.Routing.API.Services.Services.Abstractions;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Models.Routing.SharedAPIModels.Request;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Services.Services.Implementations
{
    public class RoutingService : IRoutingService
    {
        private readonly IRoutingRepository _routingRepository;
        private readonly IRoutingTimeRepository _routingTimeRepository;
        private readonly IRoutingDataRepository _routingDataRepository;

        public RoutingService(IRoutingRepository routingRepository,
                              IRoutingTimeRepository routingTimeRepository,
                              IRoutingDataRepository routingDataRepository)
        {
            _routingRepository = routingRepository;
            _routingTimeRepository = routingTimeRepository;
            _routingDataRepository = routingDataRepository;
        }

        public async Task<RoutingResponseViewModel> Calculate(RoutingRequestViewModel model)
        {
            var timesTask = _routingTimeRepository.Fetch(model.Start, model.End, model.MachineName);
            var dataTask = _routingDataRepository.Fetch(model.Start, model.End, model.MachineName);

            await Task.WhenAll(timesTask, dataTask);

            var timesTaskResult = timesTask.TryGetResult();
            var dataTaskResult = dataTask.TryGetResult();
            
            var dims = dataTaskResult.Data.Select(m => m.Dimension);

            var routings = await _routingRepository.Fetch(dims);

            var data = new List<RoutingsDataResponseViewModel>();

            foreach (var item in dataTaskResult.Data)
            {
                var time = timesTaskResult.Data.FirstOrDefault(m => m.Dimension == item.Dimension)
                           ?? throw new APIErrorException($"A {item.Dimension} dimenzióhoz nincs feltöltve routing idő");
                var routing = routings.FirstOrDefault(m => m.Dimension == item.Dimension)
                              ?? throw new APIErrorException($"A {item.Dimension} dimenzióhoz nincs feltöltve routing");

                data.Add(new RoutingsDataResponseViewModel
                {
                    Dimension = item.Dimension,
                    ActualRouting = item.AllLength,
                    ExpectedRouting = time.ProductionMinutes / routing.CycleQuantityPerMinute * 1000
                });
            }

            return new RoutingResponseViewModel 
            {
                Start = model.Start,
                End = model.End,
                Data = data,
            };
        }
    }
}
