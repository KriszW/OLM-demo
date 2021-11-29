using Microsoft.EntityFrameworkCore;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations
{
    public class MoneyExchangeRateRepository : IMoneyExchangeRateRepository
    {
        private readonly MoneyExchangeRatesDbContext _dbContext;

        public MoneyExchangeRateRepository(MoneyExchangeRatesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
            => _dbContext.Currencies.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.Currencies.AnyAsync(c => c.ID.GetValueOrDefault() == id);

        private Task<bool> AnyWithISOCode(string ISOCode)
            => _dbContext.Currencies.AnyAsync(c => c.ISOCode == ISOCode);

        private Task<bool> AnyWithISOCodeExceptThisID(int id,string ISOCode)
            => _dbContext.Currencies.AnyAsync(c => c.ISOCode == ISOCode && c.ID.GetValueOrDefault() != id);

        public Task<CurrencyModel> GetByID(int id) => _dbContext.Currencies.FirstOrDefaultAsync(m => m.ID.GetValueOrDefault() == id);

        public Task<List<string>> All()
            => _dbContext.Currencies.Select(m => m.ISOCode).Distinct().ToListAsync();

        public async Task<Paginated<CurrencyModel>> GetPaginated(int skip, int take)
        {
            if (take <= 0) return default;

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.Currencies.Skip(skip).Take(take);

            return new Paginated<CurrencyModel>(actualPage, take, allCount, data);
        }

        public async Task Add(CurrencyModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true) throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(CurrencyModel.ID));

            if (await AnyWithISOCode(model.ISOCode) == true) throw new UniqueDataAlreadyExistsException<string>(model.ISOCode, nameof(CurrencyModel.ISOCode));

            await _dbContext.Currencies.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, CurrencyModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false) throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(CurrencyModel.ID));

            if (await AnyWithISOCodeExceptThisID(id, model.ISOCode) == true) throw new UniqueDataAlreadyExistsException<string>(model.ISOCode, nameof(CurrencyModel.ISOCode));

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false) throw new NotFoundByValueException<int>(id, nameof(CurrencyModel.ID));

            var data = await GetByID(id);

            _dbContext.Currencies.Remove(data);
            await _dbContext.SaveChangesAsync();
        }
    }
}
