using Dapper;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Models;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Repositories.Implementations
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
            var sql = $"SELECT top(1) BundleID FROM {_dbTables.DestinationBundle} order by ID desc";

            using (_dbConnection)
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<string>(sql);
            }
        }

        public async Task UploadNewModels(IEnumerable<DestinationBundleModel> bundles)
        {   
            using (_dbConnection)
            {
                foreach (var bundle in bundles)
                {
                    await UploadNewModel(bundle);
                }
            }
        }

        private async Task UploadNewModel(DestinationBundleModel bundle)
        {
            var sql = $"INSERT INTO {_dbTables.DestinationBundle} (BundleID) VALUES (@BundleID)";

            var parameters = new DynamicParameters();
            parameters.Add("@BundleID", bundle.BundleID, DbType.String);

            await _dbConnection.ExecuteAsync(sql, parameters);
            var IDofUploadedEntity = await GetPrimaryKeyIDOfLatestUploadedBundle(bundle.BundleID);

            if (IDofUploadedEntity.HasValue == true)
            {
                await UploadItemNumbers(bundle.Itemnumbers, IDofUploadedEntity.Value);
            }
        }

        private async Task<int?> GetPrimaryKeyIDOfLatestUploadedBundle(string bundleID)
        {
            var sql = $"SELECT ID FROM {_dbTables.DestinationBundle} where BundleID = @BundleID";

            var parameters = new DynamicParameters();
            parameters.Add("@BundleID", bundleID, DbType.String);

            return await _dbConnection.QueryFirstOrDefaultAsync<int?>(sql, parameters);
        }

        private async Task UploadItemNumbers(IEnumerable<DestinationItemnumberModel> models, int bundleID)
        {
            var sql = $"INSERT INTO {_dbTables.DestinationItemNumbers} (Itemnumber, BundleItemnumbersModelID) VALUES (@Itemnumber, @BundleItemnumbersModelID)";

            var parameters = models.Select(m =>
            {
                var tempParams = new DynamicParameters();
                tempParams.Add("@Itemnumber", m.Itemnumber, DbType.String);
                tempParams.Add("@BundleItemnumbersModelID", bundleID, DbType.Int32);
                return tempParams;
            });

            await _dbConnection.ExecuteAsync(sql, parameters);
        }
    }
}
