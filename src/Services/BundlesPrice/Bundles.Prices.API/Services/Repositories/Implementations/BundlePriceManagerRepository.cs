using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations
{
    public class BundlePriceManagerRepository : IBundlePriceManagerRepository
    {
        private readonly BundlePriceDbContext _dbContext;

        public BundlePriceManagerRepository(BundlePriceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
            => _dbContext.BundlePrices.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.BundlePrices.AnyAsync(b => b.ID.GetValueOrDefault() == id);

        private Task<bool> AnyWithItemNumForVendorID(string dimension, string vendorID)
            => _dbContext.BundlePrices.AnyAsync(b => b.RawMaterialItemNumber == dimension && b.VendorID == vendorID);

        private Task<bool> AnyWithItemNumForVendorIDExceptWithThisID(int id, string dimension, string vendorID)
            => _dbContext.BundlePrices.AnyAsync(b => b.ID.GetValueOrDefault() != id && b.RawMaterialItemNumber == dimension && b.VendorID == vendorID);

        public Task<BundlePriceModel> GetByID(int id)
            => _dbContext.BundlePrices.FirstOrDefaultAsync(b => b.ID.GetValueOrDefault() == id);

        public async Task<Paginated<BundlePriceModel>> GetPagineted(int skip, int take)
        {
            if (take <= 0) return default;

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.BundlePrices.Skip(skip).Take(take).OrderBy(m => m.ID);

            return new Paginated<BundlePriceModel>(actualPage, take, allCount, data);
        }

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false) throw new NotFoundByValueException<int>(id, nameof(BundlePriceModel.ID));

            var data = await GetByID(id);

            _dbContext.BundlePrices.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, BundlePriceModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false) throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(BundlePriceModel.ID));

            if (await AnyWithItemNumForVendorIDExceptWithThisID(id, model.RawMaterialItemNumber, model.VendorID) == true) throw new UniqueDataAlreadyExistsException<string>(model.VendorID, nameof(BundlePriceModel.VendorID));

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Upload(BundlePriceModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true) throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(BundlePriceModel.ID));

            if (await AnyWithItemNumForVendorID(model.RawMaterialItemNumber, model.VendorID) == true) throw new UniqueDataAlreadyExistsException<string>(model.VendorID, nameof(BundlePriceModel.VendorID));

            await _dbContext.BundlePrices.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }
    }
}
