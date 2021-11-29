using Microsoft.EntityFrameworkCore;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions;
using OLM.Services.MoneyExchangeRate.API.ViewModels;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly MoneyExchangeRatesDbContext _dbContext;

        public ExchangeRateRepository(MoneyExchangeRatesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
            => _dbContext.ExchangeRates.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.ExchangeRates.AnyAsync(c => c.ID.GetValueOrDefault() == id);

        private Task<bool> AnyWithISOCodeForSourceISOCode(string sourceISOCode,string ISOCode)
            => _dbContext.Currencies.Include(c => c.Rates).AnyAsync(c => c.ISOCode == sourceISOCode && c.Rates.Any(e => e.DestISOCode == ISOCode));

        private Task<bool> AnyWithISOCodeForSourceISOCodeExceptThisID(int id,string sourceISOCode, string ISOCode)
            => _dbContext.Currencies.Include(c => c.Rates).AnyAsync(c => c.ISOCode == sourceISOCode && c.Rates.Any(e => e.DestISOCode == ISOCode && e.ID.GetValueOrDefault() != id));

        private Task<CurrencyModel> GetByISOCode(string ISOCode)
            => _dbContext.Currencies.Include(c => c.Rates).FirstOrDefaultAsync(c => c.ISOCode == ISOCode);

        public Task<ExchangeRateModel> GetByID(int id)
            => _dbContext.ExchangeRates.FirstOrDefaultAsync(e => e.ID.GetValueOrDefault() == id);

        public async Task<Paginated<ExchangeRateModel>> GetPaginatedForISOCode(PaginatedExchangeRateViewModel model)
        {
            if (model.Take <= 0) return default;

            var currency = await _dbContext.Currencies.Include(c => c.Rates).FirstOrDefaultAsync(c => c.ISOCode == model.ISOCode);

            if (currency == default) return default;

            var data = currency.Rates?.Skip(model.Skip).Take(model.Take);

            return await BuildPaginationModel(model, data);
        }

        private async Task<Paginated<ExchangeRateModel>> BuildPaginationModel(PaginatedExchangeRateViewModel model, IEnumerable<ExchangeRateModel> data)
        {
            var allCount = await GetAllCount();

            var actualPage = model.Skip / model.Take;

            return new Paginated<ExchangeRateModel>(actualPage, model.Take, allCount, data);
        }

        public async Task Modify(ModifyExchangeRateForCurrencyViewModel model)
        {
            if (await AnyWithID(model.Data.ID.GetValueOrDefault()) == false) 
                throw new NotFoundByValueException<int>(model.Data.ID.GetValueOrDefault(), nameof(CurrencyModel.ID));

            if (await AnyWithISOCodeForSourceISOCodeExceptThisID(model.Data.ID.GetValueOrDefault(), model.SourceISOCode, model.Data.DestISOCode) == true) 
                throw new UniqueDataAlreadyExistsException<string>(model.Data.DestISOCode, nameof(ExchangeRateModel.DestISOCode));

            _dbContext.Entry(model.Data).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(string ISOCode, ExchangeRateModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true) 
                throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(ExchangeRateModel.ID));

            if (await AnyWithISOCodeForSourceISOCode(ISOCode, model.DestISOCode) == true) 
                throw new UniqueDataAlreadyExistsException<string>(model.DestISOCode, nameof(ExchangeRateModel.DestISOCode));

            var currency = await GetByISOCode(ISOCode);

            currency.Rates.Add(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false) throw new NotFoundByValueException<int>(id, nameof(CurrencyModel.ID));

            var data = await GetByID(id);

            _dbContext.ExchangeRates.Remove(data);
            await _dbContext.SaveChangesAsync();
        }
    }
}
