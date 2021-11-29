using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.Routing.Abstractions
{
    public interface IRoutingAggregator
    {
        Task<AggregatedRoutingViewModel> Aggregate(string machineID);
    }
}
