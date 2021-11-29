using Dapper;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Models;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Repositories.Implementations
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
            var sql = $"SELECT top(1) Date FROM {_dbTables.Destination} order by ID desc";

            using (_dbConnection)
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<DateTime>(sql);
            }
        }

        public async Task UploadNewModels(IEnumerable<DailyReportModel> models)
        {
            var sql = $"INSERT INTO {_dbTables.Destination} " +
                      $"([Date],[Dimension],[Length],[LengthOfWaste],[LengthOfFS])" +
                      $"VALUES (@Date,@Dimension,@Length,@LengthOfWaste,@LengthOfFS)";

            using (_dbConnection)
            {
                var parameters = models.Select(m =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@Date", m.Date, DbType.DateTime);
                    tempParams.Add("@Dimension", m.Dimension, DbType.String);
                    tempParams.Add("@Length", m.Length, DbType.Double);
                    tempParams.Add("@LengthOfWaste", m.Waste, DbType.Double);
                    tempParams.Add("@LengthOfFS", m.FS, DbType.Double);
                    return tempParams;
                });

                await _dbConnection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
