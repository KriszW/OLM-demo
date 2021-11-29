using OLM.Services.CategoryBulbs.API.Models;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Services.Repositories.Abstractions
{
    public interface IItemNumberCategoryRepository
    {
        Task<Paginated<ItemnumberCategoryModel>> Search(string categoryQuery, int skip, int take);

        Task<Paginated<ItemnumberCategoryModel>> GetPaginated(int skip, int take);

        Task<ItemnumberCategoryModel> GetByID(int id);

        Task Upload(ItemnumberCategoryModel model);

        Task Update(int id, ItemnumberCategoryModel model);

        Task Delete(int id);
    }
}
