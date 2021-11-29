using Dapper;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Models;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Implementations
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly DbTables _dbTables;

        public DestinationRepository(IDbConnection dbConnection, DbTables dbTables)
        {
            _dbConnection = dbConnection;
            _dbTables = dbTables;
        }

        public async Task<DateTime> GetLatestDate()
        {
            var sql = $"SELECT top(1) FinishedDate FROM {_dbTables.Destination} order by ID desc";

            using (_dbConnection)
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<DateTime>(sql);
            }
        }

        public async Task UploadNewModels(IEnumerable<BundleModel> models)
        {
            var sql = $"INSERT INTO {_dbTables.Destination} " +
                      $"([FinishedDate],[Dimension],[MachineName])" +
                      $" VALUES (@FinishedDate,@Dimension,@MachineName)";

            using (_dbConnection)
            {
                var parameters = models.Select(m =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@FinishedDate", m.FinishedDate, DbType.DateTime);
                    tempParams.Add("@Dimension", m.Dimension, DbType.String);
                    tempParams.Add("@MachineName", m.MachineName, DbType.String);
                    return tempParams;
                });

                await _dbConnection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
