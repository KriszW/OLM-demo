using Microsoft.EntityFrameworkCore;
using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Models;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations
{
    public class ItemnumberCategoryRepository : IItemNumberCategoryRepository
    {
        private CategoryBulbsDbContext _dbContext;

        public ItemnumberCategoryRepository(CategoryBulbsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Task<long> GetAllCount()
            => _dbContext.Categories.LongCountAsync();

        private Task<long> GetAllCount(string categoryQuery)
            => StartSearch(categoryQuery).LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.Categories.AnyAsync(b => b.ID.GetValueOrDefault() == id);

        private Task<bool> AnyWithItemNumber(string itemNumber)
            => _dbContext.Categories.AnyAsync(b => b.Itemnumber == itemNumber);

        private Task<bool> AnyWithItemnumberExceptWithThisID(int id, string itemNumber)
            => _dbContext.Categories.AnyAsync(b => b.ID.GetValueOrDefault() != id && b.Itemnumber == itemNumber);

        private IQueryable<ItemnumberCategoryModel> StartSearch(string categoryQuery)
            => _dbContext.Categories.Where(m => m.Itemnumber.Contains(categoryQuery));

        public async Task<Paginated<ItemnumberCategoryModel>> Search(string categoryQuery, int skip, int take)
        {
            if (take <= 0) return default;

            var allCount = await GetAllCount(categoryQuery);

            var actualPage = skip / take;

            var data = StartSearch(categoryQuery).Skip(skip).Take(take);

            return new Paginated<ItemnumberCategoryModel>(actualPage, take, allCount, data);
        }

        public Task<ItemnumberCategoryModel> GetByID(int id)
            => _dbContext.Categories.FirstOrDefaultAsync(b => b.ID.GetValueOrDefault() == id);

        public async Task<Paginated<ItemnumberCategoryModel>> GetPaginated(int skip, int take)
        {
            if (take <= 0) return default;

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.Categories.Skip(skip).Take(take);

            return new Paginated<ItemnumberCategoryModel>(actualPage, take, allCount, data);
        }

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false)
            {
                throw new NotFoundByValueException<int>(id, nameof(ItemnumberCategoryModel.ID));
            }

            var data = await GetByID(id);

            _dbContext.Categories.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int id, ItemnumberCategoryModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false)
            {
                throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(ItemnumberCategoryModel.ID));
            }

            if (await AnyWithItemnumberExceptWithThisID(id, model.Itemnumber) == true)
            {
                throw new UniqueDataAlreadyExistsException<string>(model.Itemnumber, nameof(ItemnumberCategoryModel.Itemnumber));
            }

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Upload(ItemnumberCategoryModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true)
            {
                throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(ItemnumberCategoryModel.ID));
            }

            if (await AnyWithItemNumber(model.Itemnumber) == true)
            {
                throw new UniqueDataAlreadyExistsException<string>(model.Itemnumber, nameof(ItemnumberCategoryModel.Itemnumber));
            }

            await _dbContext.Categories.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }
    }
}
