using Dapper;
using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Implementations
{
    public class DestinationBundlesRepository : IDestinationBundlesRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly DbTables _dbTables;

        public DestinationBundlesRepository(IDbConnection dbConnection, DbTables tables)
        {
            _dbConnection = dbConnection;
            _dbTables = tables;
        }

        public async Task<DateTime> GetLatestBundleFinishedDate()
        {
            var sql = $"select top(1) FinishedDate from {_dbTables.Destination} order by FinishedDate desc";

            using (_dbConnection)
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<DateTime>(sql);
            }
        }

        public async Task UploadBundles(IEnumerable<BundleModel> models)
        {
            var sql = $"INSERT INTO {_dbTables.Destination} " +
                      $"([BundleID],[Input],[Produced],[Primary],[Secondary],[FS],[Waste],[Dimension],[Quality],[VendorName],[SawmillName],[MachineName],[FinishedDate])" +
                      $"VALUES (@BundleID, @Input, @Produced, @Primary, @Secondary, @FS, @Waste,@Dimension,@Quality,@VendorName,@SawmillName, @MachineName, @FinishedDate)";

            using (_dbConnection)
            {
                var parameters = models.Select(m =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@BundleID", m.BundleID, DbType.String);
                    tempParams.Add("@Input", m.Input, DbType.Double);
                    tempParams.Add("@Produced", m.Produced, DbType.Double);
                    tempParams.Add("@Primary", m.Primary, DbType.Double);
                    tempParams.Add("@Secondary", m.Secondary, DbType.Double);
                    tempParams.Add("@FS", m.FS, DbType.Double);
                    tempParams.Add("@Waste", m.Waste, DbType.Double);
                    tempParams.Add("@Dimension", m.Dimension, DbType.String);
                    tempParams.Add("@Quality", m.Quality, DbType.String);
                    tempParams.Add("@VendorName", m.VendorName, DbType.String);
                    tempParams.Add("@SawmillName", m.SawmillName, DbType.String);
                    tempParams.Add("@MachineName", m.MachineName, DbType.String);
                    tempParams.Add("@FinishedDate", m.FinishedDate, DbType.DateTime);
                    return tempParams;
                });

                await _dbConnection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
