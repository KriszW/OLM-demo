using OLM.Shared.Models.Routing.SharedAPIModels.Request;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Services.Services.Abstractions
{
    public interface IRoutingService
    {
        Task<RoutingResponseViewModel> Calculate(RoutingRequestViewModel model);
    }
}
