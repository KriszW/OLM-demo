using OLM.Shared.Models.RoutingTime.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Services.Repositories.Abstractions
{
    public interface IRoutingTimeRepository
    {
        Task<RoutingTimesResponseViewModel> Fetch(DateTime start, DateTime end, string machineName);
    }
}
