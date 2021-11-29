using OLM.Services.RoutingTime.API.Models;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Services.Repositories.Abstractions
{
    public interface IProductionTimeRepository
    {
        Task<List<ProductionTimeModel>> FetchBetween(string machineName, DateTime start, DateTime end);

        Task<Paginated<WeekNumberPaginatorModelDataViewModel>> GetPaginatedForWeekNumber(int skip, int take);
        Task<Paginated<ProductionTimeModel>> GetPaginatedForData(int year, int weekNumber, int skip, int take);

        Task<ProductionTimeModel> GetByID(int id);

        Task Add(ProductionTimeModel model);
        Task Modify(int id, ProductionTimeModel model);
        Task Delete(int id);
    }
}
