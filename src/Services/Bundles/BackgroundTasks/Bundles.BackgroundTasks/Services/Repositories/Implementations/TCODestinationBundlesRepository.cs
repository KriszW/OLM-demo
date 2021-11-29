using Dapper;
using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Utilities.Settings;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Bundle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Implementations
{
    class TCODestinationBundlesRepository : ITCODestinationBundlesRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly DbTables _dbTables;

        public TCODestinationBundlesRepository(IDbConnection dbConnection, DbTables tables)
        {
            _dbConnection = dbConnection;
            _dbTables = tables;
        }

        public async Task<bool> AlreadyUploaded(string bundleID)
        {
            var sql = $"select * from {_dbTables.TCODestination} where BundleID='"+bundleID+"'";

            using (_dbConnection)
            {
                var bundle = await _dbConnection.QueryFirstOrDefaultAsync<TCOBundleModel>(sql);

                return bundle != default;
            }
        }

        public async Task<DateTime> GetLatestBundleFinishedDate()
        {
            var sql = $"select top(1) FinishedDate from {_dbTables.TCODestination} order by FinishedDate desc";

            using (_dbConnection)
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<DateTime>(sql);
            }
        }

        public async Task UploadBundles(IEnumerable<TCOBundleModel> models)
        {
            var sql = $"INSERT INTO {_dbTables.TCODestination} " +
                      $"([BundleID],[Vendor],[Sawmill],[Quality],[Input],[Good],[FS],[GoodRate],[StandardTCO],[ActualTCO],[FinishedDate],[Dimension],[MaterialNumber])" +
                      $"VALUES (@BundleID, @Vendor, @Sawmill, @Quality, @Input, @Good, @FS, @GoodRate, @StandardTCO, @ActualTCO, @FinishedDate, @Dimension, @MaterialNumber)";

            using (_dbConnection)
            {
                var parameters = models.Select(m =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@BundleID", m.BundleID, DbType.String);
                    tempParams.Add("@Vendor", m.Vendor, DbType.String);
                    tempParams.Add("@Sawmill", m.Sawmill, DbType.String);
                    tempParams.Add("@Quality", m.Quality, DbType.String);
                    tempParams.Add("@Input", m.Input, DbType.Double);
                    tempParams.Add("@Good", m.Good, DbType.Double);
                    tempParams.Add("@FS", m.FS, DbType.Double);
                    tempParams.Add("@GoodRate", m.GoodRate != double.NaN ? m.GoodRate : 0, DbType.Double);
                    tempParams.Add("@StandardTCO", m.StandardTCO, DbType.Double);
                    tempParams.Add("@ActualTCO", m.ActualTCO, DbType.Double);
                    tempParams.Add("@FinishedDate", m.FinishedDate, DbType.DateTime);
                    tempParams.Add("@Dimension", m.Dimension, DbType.String);
                    tempParams.Add("@MaterialNumber", m.MaterialNumber, DbType.String);
                    return tempParams;
                });

                await _dbConnection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
