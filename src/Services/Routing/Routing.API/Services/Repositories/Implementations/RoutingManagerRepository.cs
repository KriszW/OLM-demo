using Microsoft.EntityFrameworkCore;
using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using OLM.Services.Routing.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Services.Repositories.Implementations
{
    public class RoutingManagerRepository : IRoutingManagerRepository
    {
        private readonly RoutingDbContext _dbContext;

        public RoutingManagerRepository(RoutingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
            => _dbContext.Routing.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.Routing.AnyAsync(b => b.ID.GetValueOrDefault() == id);

        public Task<RoutingModel> GetByID(int id)
            => _dbContext.Routing.FirstOrDefaultAsync(m => m.ID.GetValueOrDefault() == id);

        public async Task<Paginated<RoutingModel>> GetPaginated(int skip, int take)
        {
            if (take <= 0) return default;

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.Routing.Skip(skip).Take(take);

            return new Paginated<RoutingModel>(actualPage, take, allCount, data);
        }

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false)
            {
                throw new NotFoundByValueException<int>(id, nameof(RoutingModel.ID));
            }

            var data = await GetByID(id);

            _dbContext.Routing.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(RoutingModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true)
            {
                throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(RoutingModel.ID));
            }

            await _dbContext.Routing.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, RoutingModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false)
            {
                throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(RoutingModel.ID));
            }

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
