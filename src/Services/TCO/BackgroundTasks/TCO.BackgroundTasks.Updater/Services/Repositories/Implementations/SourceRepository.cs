using Dapper;
using OLM.Services.TCO.BackgroundTasks.Updater.Models;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.TCO.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.TCO.BackgroundTasks.Updater.Services.Repositories.Implementations
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

        public async Task<DateTime> GetDatetimeForBundle(string bundleID)
        {
            var sql = $"select FinishedDate from {_dbTables.Source} where BundleID = @bundleID";

            using (_dbConnection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@bundleID", bundleID, DbType.String);

                return await _dbConnection.QueryFirstOrDefaultAsync<DateTime>(sql, parameters);
            }
        }

        public async Task<IEnumerable<TCODataSourceModel>> GetModelsFrom(DateTime date)
        {
            var sql = $"select top(1000) * from {_dbTables.Source} where FinishedDate > @date order by FinishedDate";

            using (_dbConnection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@date", date, DbType.DateTime);

                return await _dbConnection.QueryAsync<TCODataSourceModel>(sql, parameters);
            }
        }
    }
}
