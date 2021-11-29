using OLM.Services.TCO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Repositories.Abstractions
{
    public interface ITCODataRepository
    {
        Task<TCODataModel> GetByBundleID(string bundleID);
        Task<List<TCODataModel>> GetByBundleIDs(IEnumerable<string> bundleIDs);
    }
}
