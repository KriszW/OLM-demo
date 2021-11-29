using Microsoft.EntityFrameworkCore;
using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Services.Repositories.Implementations
{
    public class PausesRepository : IPausesRepository
    {
        private readonly RoutingTimeDbContext _dbContext;

        public PausesRepository(RoutingTimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<PauseModel>> FetchBetween(string machineName, DateTime start, DateTime end)
            => _dbContext.Pauses.Where(m => m.MachineName == machineName && m.Start >= start && m.End <= end).ToListAsync();

        private Task<long> GetAllCount()
            => _dbContext.Pauses.LongCountAsync();

        private Task<bool> AnyWithID(int id)
            => _dbContext.Pauses.AnyAsync(b => b.ID.GetValueOrDefault() == id);

        public Task<PauseModel> GetByID(int id)
            => _dbContext.Pauses.FirstOrDefaultAsync(m => m.ID.GetValueOrDefault() == id);

        public async Task<Paginated<WeekNumberPaginatorModelDataViewModel>> GetPaginatedForWeekNumber(int skip, int take)
        {
            if (take <= 0) return default;

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var rawData = await _dbContext.Pauses.Skip(skip).Take(take).ToListAsync();

            var data = CreateWeekNumberPaginatorData(rawData);

            return new Paginated<WeekNumberPaginatorModelDataViewModel>(actualPage, take, allCount, data);
        }

        private List<WeekNumberPaginatorModelDataViewModel> CreateWeekNumberPaginatorData(List<PauseModel> data)
        {
            var groupedData = data.GroupBy(m => m.WeekNumber);

            var output = new List<WeekNumberPaginatorModelDataViewModel>();

            foreach (var item in groupedData)
            {
                if (item.Any() == true)
                {
                    var model = new WeekNumberPaginatorModelDataViewModel
                    {
                        WeekNumber = item.Key,
                        Year = item.First().Start.Year,
                        Start = item.Min(m => m.Start),
                        End = item.Max(m => m.End),
                    };

                    output.Add(model); 
                }
            }

            return output;
        }

        public async Task<Paginated<PauseModel>> GetPaginatedForData(int year, int weekNumber, int skip, int take)
        {
            if (take <= 0) return default;

            var allCount = await GetAllCount();

            var actualPage = skip / take;

            var data = _dbContext.Pauses.Where(m => m.WeekNumber == weekNumber && m.Start.Year == year).Skip(skip).Take(take);

            return new Paginated<PauseModel>(actualPage, take, allCount, data);
        }

        public async Task Delete(int id)
        {
            if (await AnyWithID(id) == false)
            {
                throw new NotFoundByValueException<int>(id, nameof(PauseModel.ID));
            }

            var data = await GetByID(id);

            _dbContext.Pauses.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, PauseModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == false)
            {
                throw new NotFoundByValueException<int>(model.ID.GetValueOrDefault(), nameof(PauseModel.ID));
            }

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(PauseModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true)
            {
                throw new PrimaryKeyAlreadyExistsException<int>(model.ID.GetValueOrDefault(), nameof(PauseModel.ID));
            }

            await _dbContext.Pauses.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }
    }
}
