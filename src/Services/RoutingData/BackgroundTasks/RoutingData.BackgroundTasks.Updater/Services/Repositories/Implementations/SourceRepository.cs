using Dapper;
using OLM.Services.RoutingData.BackgroundTasks.Updater.Models;
using OLM.Services.RoutingData.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.RoutingData.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.BackgroundTasks.Updater.Services.Repositories.Implementations
{
    public class SourceRepository : ISourceRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly DbTables _dbTables;

        public SourceRepository(IDbConnection dbConnection, DbTables dbTables)
        {
            _dbConnection = dbConnection;
            _dbTables = dbTables;
        }

        public async Task<IEnumerable<RoutingDataModel>> GetModelsFrom(DateTime date)
        {
            var sql = $"select top(1000) * from {_dbTables.Source} where FinishedDate > @date order by FinishedDate";

            using (_dbConnection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@date", date, DbType.DateTime);

                return await _dbConnection.QueryAsync<RoutingDataModel>(sql, parameters);
            }
        }
    }
}
