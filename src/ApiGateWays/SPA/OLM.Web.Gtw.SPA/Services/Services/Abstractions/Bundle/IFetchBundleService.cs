using OLM.Shared.Models.Bundles.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Bundle
{
    public interface IFetchBundleService
    {
        Task<IEnumerable<BundleWithDateAPIResponseViewModel>> GetFrom(DateTime from, DateTime to);
    }
}
