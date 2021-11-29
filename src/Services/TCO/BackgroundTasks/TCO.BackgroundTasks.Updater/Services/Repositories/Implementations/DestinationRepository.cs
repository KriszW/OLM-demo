using Dapper;
using OLM.Services.TCO.BackgroundTasks.Updater.Models;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.TCO.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.TCO.BackgroundTasks.Updater.Services.Repositories.Implementations
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

        public async Task<string> GetLatestBundleID()
        {
            var sql = $"SELECT top(1) BundleID FROM {_dbTables.Destination} order by ID desc";

            using (_dbConnection)
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<string>(sql);
            }
        }

        public async Task UploadNewModels(IEnumerable<TCODataDestinationModel> models)
        {
            var sql = $"INSERT INTO {_dbTables.Destination} " +
                      $"([Volume],[Primary],[Secondary],[BundleID],[RawMaterialItemNumber],[VendorID])" +
                      $"VALUES (@Volume,@Primary,@Secondary,@BundleID,@RawMaterialItemNumber,@VendorID)";

            using (_dbConnection)
            {
                var parameters = models.Select(m =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@Volume", m.Volume, DbType.Double);
                    tempParams.Add("@Primary", m.Primary, DbType.Double);
                    tempParams.Add("@Secondary", m.Secondary, DbType.Double);
                    tempParams.Add("@BundleID", m.BundleID, DbType.String);
                    tempParams.Add("@RawMaterialItemNumber", m.RawMaterialItemNumber, DbType.String);
                    tempParams.Add("@VendorID", m.VendorID, DbType.String);
                    return tempParams;
                });

                await _dbConnection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
