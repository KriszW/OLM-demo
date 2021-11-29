using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Bundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.TCOBundle.Abstractions
{
    public interface ITCOBundleAggregator
    {
        Task<IEnumerable<TCOBundleModel>> GetDataFrom(DateTime from, DateTime to);
    }
}
