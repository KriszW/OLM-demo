using Microsoft.EntityFrameworkCore;
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
    public class TramDataRepository : ITramDataRepository
    {
        private readonly TramDbContext _dbContext;

        public TramDataRepository(TramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
            => _dbContext.Trams.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.Trams.AnyAsync(b => b.ID.GetValueOrDefault() == id);

        private Task<bool> AnyDimensionWith(string dimension)
            => _dbContext.Dimensions.AnyAsync(b => b.Dimension == dimension);

        public async Task<Paginated<TramDataModel>> GetPaginetedPrices(int skip, int take)
        {
            if (take <= 0) return default;

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.Trams.Include(m => m.Dimension).Skip(skip).Take(take);

            return new Paginated<TramDataModel>(actualPage, take, allCount, data);
        }

        public Task<TramDataModel> GetByID(int id)
            => _dbContext.Trams.Include(m => m.Dimension).FirstOrDefaultAsync(b => b.ID.GetValueOrDefault() == id);

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false) throw new NotFoundByValueException<int>(id, nameof(TramDataModel.ID));

            var data = await GetByID(id);

            _dbContext.Trams.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, TramDataModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false) throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(TramDataModel.ID));

            if (await AnyDimensionWith(model.Dimension.Dimension) == false) throw new DataNotFoundWithSpecifiedColumnException<string>(model.Dimension.Dimension, nameof(TramDataModel.Dimension.Dimension));

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(TramDataModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true) throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(TramDataModel.ID));

            if (await AnyDimensionWith(model.Dimension.Dimension) == false) throw new DataNotFoundWithSpecifiedColumnException<string>(model.Dimension.Dimension, nameof(TramDataModel.Dimension.Dimension));

            model.Dimension = await _dbContext.Dimensions.FirstOrDefaultAsync(m => m.Dimension == model.Dimension.Dimension);

            await _dbContext.Trams.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }
    }
}
