using OLM.Services.RoutingData.API.Services.Repositories.Abstractions;
using OLM.Services.RoutingData.API.Services.Services.Abstractions;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.API.Services.Services.Implementations
{
    public class RoutingDataCalculatorService : IRoutingDataCalculatorService
    {
        private readonly IRoutingDataRepository _routingDataRepository;

        public RoutingDataCalculatorService(IRoutingDataRepository routingDataRepository)
        {
            _routingDataRepository = routingDataRepository;
        }

        public async Task<IEnumerable<RoutingDataDimensionResponseViewModel>> FetchDataForDimension(FetchRoutingDataRequestViewModel model)
        {
            var data = await _routingDataRepository.FetchData(model);

            return data.GroupBy(m => m.Dimension).Select(m => new RoutingDataDimensionResponseViewModel { Dimension = m.Key, AllLength = m.Sum(m => m.AllLength) });
        }
    }
}
