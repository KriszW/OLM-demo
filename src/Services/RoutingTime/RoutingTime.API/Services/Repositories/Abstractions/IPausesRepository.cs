using OLM.Services.RoutingTime.API.Models;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Services.Repositories.Abstractions
{
    public interface IPausesRepository
    {
        Task<List<PauseModel>> FetchBetween(string machineName,DateTime start, DateTime end);

        Task<Paginated<WeekNumberPaginatorModelDataViewModel>> GetPaginatedForWeekNumber(int skip, int take);
        Task<Paginated<PauseModel>> GetPaginatedForData(int year, int weekNumber, int skip, int take);

        Task<PauseModel> GetByID(int id);

        Task Add(PauseModel model);
        Task Modify(int id, PauseModel model);
        Task Delete(int id);
    }
}
