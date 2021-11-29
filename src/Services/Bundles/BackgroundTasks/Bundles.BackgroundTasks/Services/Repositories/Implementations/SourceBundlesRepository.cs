using Dapper;
using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Implementations
{
    public class SourceBundlesRepository : ISourceBundlesRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly DbTables _dbTables;

        public SourceBundlesRepository(IDbConnection dbConnection, DbTables tables)
        {
            _dbConnection = dbConnection;
            _dbTables = tables;
        }

        public async Task<IEnumerable<BundleModel>> QueryBundlesFromDate(DateTime latestDate)
        {
            var sql = $"select top(1000) * from {_dbTables.Source} where FinishedDate > @date order by FinishedDate";

            using (_dbConnection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@date", latestDate, DbType.DateTime);

                return await _dbConnection.QueryAsync<BundleModel>(sql, parameters);
            }
        }
    }
}
