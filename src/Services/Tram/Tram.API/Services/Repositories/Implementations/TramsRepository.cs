using Microsoft.EntityFrameworkCore;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Services.Repositories.Abstractions;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Services.Repositories.Implementations
{
    public class TramsRepository : ITramsRepository
    {
        private readonly TramDbContext _dbContext;

        public TramsRepository(TramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TramResponseViewModel>> Fetch(TramFetchRequestViewModel model)
        {
            var data = _dbContext.Trams.Include(m => m.Dimension)
                                       .Where(m => m.Date.Date >= model.Start.Date && m.Date.Date <= model.End.Date)
                                       .Select(m => new TramResponseViewModel
                                       {
                                           Date = m.Date,
                                           Dimension = m.Dimension.Dimension,
                                           NumberOfLammela = m.NumberOfLamella,
                                           NumberOfTram = m.NumberOfTrams
                                       });

            return await data.ToListAsync();
        }
    }
}
