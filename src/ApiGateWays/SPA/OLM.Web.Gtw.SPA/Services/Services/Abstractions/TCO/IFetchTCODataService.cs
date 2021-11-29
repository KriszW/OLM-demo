using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.TCO
{
    public interface IFetchTCODataService
    {
        Task<IEnumerable<TCODataAPIResponseViewModel>> FetchModels(IEnumerable<string> bundleIDs);
    }
}
