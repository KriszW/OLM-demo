﻿using Dapper;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Models;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Implementations
{
    public class ProdTimeRepository : IProdTimeRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProdTimeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> AnyDataBetween(DateTime start, DateTime end)
        {
            var sql = "select * from ProductionTimes where Start >= @start AND Start <= @end";

            using (_dbConnection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@start", start, DbType.DateTime);
                parameters.Add("@end", end, DbType.DateTime);

                var model = await _dbConnection.QueryFirstOrDefaultAsync<ProductionTimeModel>(sql, parameters);

                return model != default;
            }
        }
        public async Task<bool> AnyDataFor(DateTime date)
        {
            var sql = "select * from ProductionTimes where CONVERT(date, Start) >= CONVERT(date, @date)";

            using (_dbConnection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@date", date, DbType.DateTime);

                var model = await _dbConnection.QueryFirstOrDefaultAsync<ProductionTimeModel>(sql, parameters);

                return model != default;
            }
        }

        public async Task Upload(IEnumerable<ProductionTimeModel> models)
        {
            var sql = $"INSERT INTO ProductionTimes " +
                      $"([Start],[End],[Day],[WeekNumber],[MachineName])" +
                      $" VALUES (@Start,@End,@Day,@WeekNumber,@MachineName)";

            using (_dbConnection)
            {
                var parameters = models.Select(m =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@Start", m.Start, DbType.DateTime);
                    tempParams.Add("@End", m.End, DbType.DateTime);
                    tempParams.Add("@Day", m.Day.ToString(), DbType.String);
                    tempParams.Add("@WeekNumber", m.WeekNumber, DbType.String);
                    tempParams.Add("@MachineName", m.MachineName, DbType.String);
                    return tempParams;
                });

                await _dbConnection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
