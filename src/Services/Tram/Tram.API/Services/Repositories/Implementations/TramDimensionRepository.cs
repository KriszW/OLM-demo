using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Services.Repositories.Implementations
{
    public class TramDimensionRepository : ITramDimensionRepository
    {
        private readonly TramDbContext _dbContext;

        public TramDimensionRepository(TramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
            => _dbContext.Dimensions.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.Dimensions.AnyAsync(b => b.ID.GetValueOrDefault() == id);

        private Task<bool> AnyWithDimension(string dimension)
            => _dbContext.Dimensions.AnyAsync(b => b.Dimension == dimension);

        private Task<bool> AnyWithDimensionExceptWithThisID(int id, string dimension)
            => _dbContext.Dimensions.AnyAsync(b => b.ID.GetValueOrDefault() != id && b.Dimension == dimension);

        public async Task<Paginated<TramDimensionModel>> GetPagineted(int skip, int take)
        {
            if (take <= 0)
            {
                return default;
            }

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.Dimensions.Skip(skip).Take(take);

            return new Paginated<TramDimensionModel>(actualPage, take, allCount, data);
        }

        public Task<TramDimensionModel> GetPrice(string dimension)
            => _dbContext.Dimensions.FirstOrDefaultAsync(b => b.Dimension == dimension);

        public Task<List<TramDimensionModel>> GetPricesForDimensions(IEnumerable<string> dimensions)
            => _dbContext.Dimensions.Where(b => dimensions.Any(d => d == b.Dimension)).ToListAsync();

        public Task<TramDimensionModel> GetByID(int id)
            => _dbContext.Dimensions.FirstOrDefaultAsync(b => b.ID.GetValueOrDefault() == id);

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false)
            {
                throw new NotFoundByValueException<int>(id, nameof(TramDimensionModel.ID));
            }

            var data = await GetByID(id);

            _dbContext.Dimensions.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, TramDimensionModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false)
            {
                throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(TramDimensionModel.ID));
            }

            if (await AnyWithDimensionExceptWithThisID(id, model.Dimension) == true)
            {
                throw new UniqueDataAlreadyExistsException<string>(model.Dimension, nameof(TramDimensionModel.Dimension));
            }

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(TramDimensionModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true)
            {
                throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(TramDimensionModel.ID));
            }

            if (await AnyWithDimension(model.Dimension) == true)
            {
                throw new UniqueDataAlreadyExistsException<string>(model.Dimension, nameof(TramDimensionModel.Dimension));
            }

            await _dbContext.Dimensions.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<string>> GetAllDimension()
            => _dbContext.Dimensions.Select(m => m.Dimension).Distinct().ToListAsync();
    }
}
