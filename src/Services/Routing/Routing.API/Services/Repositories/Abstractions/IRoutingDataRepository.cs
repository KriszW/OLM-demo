using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Services.Repositories.Abstractions
{
    public interface IRoutingDataRepository
    {
        Task<RoutingDataResponseViewModel> Fetch(DateTime start, DateTime end, string machineName);
    }
}
