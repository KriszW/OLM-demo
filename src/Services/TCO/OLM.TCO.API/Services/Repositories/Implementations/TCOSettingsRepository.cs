using Microsoft.EntityFrameworkCore;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Repositories.Implementations
{
    public class TCOSettingsRepository : ITCOSettingsRepository
    {
        private TCODataDbContext _dbContext;

        public TCOSettingsRepository(TCODataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
            => _dbContext.TCOConstansValues.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.TCOConstansValues.AnyAsync(b => b.ID.GetValueOrDefault() == id);

        private Task<bool> AnyWithDimension(string dimension)
            => _dbContext.TCOConstansValues.AnyAsync(b => b.RawMaterialItemNumber == dimension);

        private Task<bool> AnyWithDimensionExceptWithThisID(int id, string dimension)
            => _dbContext.TCOConstansValues.AnyAsync(b => b.ID.GetValueOrDefault() != id && b.RawMaterialItemNumber == dimension);

        public Task<TCOValueSettingsModel> GetByDimension(string dimension)
            => _dbContext.TCOConstansValues.FirstOrDefaultAsync(t => t.RawMaterialItemNumber == dimension);

        public Task<TCOValueSettingsModel> GetByID(int id)
            => _dbContext.TCOConstansValues.FirstOrDefaultAsync(t => t.ID.GetValueOrDefault() == id);

        public Task<List<TCOValueSettingsModel>> GetForDimensions(IEnumerable<string> dimensions)
            => _dbContext.TCOConstansValues.Where(t => dimensions.Any(d=> d == t.RawMaterialItemNumber)).ToListAsync();

        public async Task<Paginated<TCOValueSettingsModel>> GetPaginated(int skip, int take)
        {
            if (take <= 0)
            {
                return default;
            }

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.TCOConstansValues.Skip(skip).Take(take);

            return new Paginated<TCOValueSettingsModel>(actualPage, take, allCount, data);
        }

        public async Task Add(TCOValueSettingsModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true)
            {
                throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(TCOValueSettingsModel.ID));
            }

            if (await AnyWithDimension(model.RawMaterialItemNumber) == true)
            {
                throw new UniqueDataAlreadyExistsException<string>(model.RawMaterialItemNumber, nameof(TCOValueSettingsModel.RawMaterialItemNumber));
            }

            await _dbContext.TCOConstansValues.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false)
            {
                throw new NotFoundByValueException<int>(id, nameof(TCOValueSettingsModel.ID));
            }

            var data = await GetByID(id);

            _dbContext.TCOConstansValues.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, TCOValueSettingsModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false)
            {
                throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(TCOValueSettingsModel.ID));
            }

            if (await AnyWithDimensionExceptWithThisID(id, model.RawMaterialItemNumber) == true)
            {
                throw new UniqueDataAlreadyExistsException<string>(model.RawMaterialItemNumber, nameof(TCOValueSettingsModel.RawMaterialItemNumber));
            }

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
