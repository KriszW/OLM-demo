using Dapper;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Models;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Repositories.Implementations
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

        public async Task<IEnumerable<DailyReportModel>> GetModelsFrom(DateTime date)
        {
            var sql = $"select top(1000) * from {_dbTables.Source} where Date > @date order by Date";

            using (_dbConnection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@date", date, DbType.DateTime);

                return await _dbConnection.QueryAsync<DailyReportModel>(sql, parameters);
            }
        }
    }
}
