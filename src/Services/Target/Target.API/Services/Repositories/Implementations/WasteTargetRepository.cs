using Microsoft.EntityFrameworkCore;
using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Models;
using OLM.Services.Target.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Services.Repositories.Implementations
{
    public class WasteTargetRepository : IWasteTargetRepository
    {
        private readonly TargetDbContext _dbContext;

        public WasteTargetRepository(TargetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
           => _dbContext.Targets.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.Targets.AnyAsync(b => b.ID.GetValueOrDefault() == id);

        private Task<bool> AnyWithDimension(string dimension)
            => _dbContext.Targets.AnyAsync(b => b.Dimension == dimension);

        private Task<bool> AnyWithDimensionExceptWithThisID(int id, string dimension)
            => _dbContext.Targets.AnyAsync(b => b.ID.GetValueOrDefault() != id && b.Dimension == dimension);

        public async Task<Paginated<WasteTargetDataModel>> Paginate(int skip, int take)
        {
            if (take <= 0)
            {
                return default;
            }

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.Targets.Skip(skip).Take(take);

            return new Paginated<WasteTargetDataModel>(actualPage, take, allCount, data);
        }

        public Task<WasteTargetDataModel> GetByID(int id)
            => _dbContext.Targets.FirstOrDefaultAsync(b => b.ID.GetValueOrDefault() == id);

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false)
            {
                throw new NotFoundByValueException<int>(id, nameof(WasteTargetDataModel.ID));
            }

            var data = await GetByID(id);

            _dbContext.Targets.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, WasteTargetDataModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false)
            {
                throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(WasteTargetDataModel.ID));
            }

            if (await AnyWithDimensionExceptWithThisID(id, model.Dimension) == true)
            {
                throw new UniqueDataAlreadyExistsException<string>(model.Dimension, nameof(WasteTargetDataModel.Dimension));
            }

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(WasteTargetDataModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true)
            {
                throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(WasteTargetDataModel.ID));
            }

            if (await AnyWithDimension(model.Dimension) == true)
            {
                throw new UniqueDataAlreadyExistsException<string>(model.Dimension, nameof(WasteTargetDataModel.Dimension));
            }

            await _dbContext.Targets.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }
    }
}
