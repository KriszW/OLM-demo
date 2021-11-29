using Dapper;
using OLM.Services.RoutingData.BackgroundTasks.Updater.Models;
using OLM.Services.RoutingData.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.RoutingData.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.BackgroundTasks.Updater.Services.Repositories.Implementations
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

        public async Task UploadNewModels(IEnumerable<RoutingDataModel> models)
        {
            var sql = $"INSERT INTO {_dbTables.Destination} " +
                      $"([FinishedDate],[Dimension],[AllLength],[BundleID],[MachineName])" +
                      $" VALUES (@FinishedDate,@Dimension,@AllLength,@BundleID,@MachineName)";

            using (_dbConnection)
            {
                var parameters = models.Select(m =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@BundleID", m.BundleID, DbType.String);
                    tempParams.Add("@FinishedDate", m.FinishedDate, DbType.DateTime);
                    tempParams.Add("@Dimension", m.Dimension, DbType.String);
                    tempParams.Add("@AllLength", m.AllLength, DbType.Double);
                    tempParams.Add("@MachineName", m.MachineName, DbType.String);
                    return tempParams;
                });

                await _dbConnection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
