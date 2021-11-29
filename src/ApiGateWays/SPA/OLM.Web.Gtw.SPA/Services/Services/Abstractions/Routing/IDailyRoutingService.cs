using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Routing
{
    public interface IDailyRoutingService
    {
        Task<RoutingResponseViewModel> FetchForDay(string machineID);
    }
}
