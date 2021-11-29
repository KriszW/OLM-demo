using OLM.Services.Target.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Services.Repositories.Abstractions
{
    public interface ITargetRepository
    {
        Task<WasteTargetDataModel> GetByDimension(string dimension);
        Task<List<WasteTargetDataModel>> GetByDimension(IEnumerable<string> dimensions);
    }
}
