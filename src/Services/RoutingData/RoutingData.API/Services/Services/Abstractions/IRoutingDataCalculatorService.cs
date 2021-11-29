using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.API.Services.Services.Abstractions
{
    public interface IRoutingDataCalculatorService
    {
        Task<IEnumerable<RoutingDataDimensionResponseViewModel>> FetchDataForDimension(FetchRoutingDataRequestViewModel model);
    }
}
