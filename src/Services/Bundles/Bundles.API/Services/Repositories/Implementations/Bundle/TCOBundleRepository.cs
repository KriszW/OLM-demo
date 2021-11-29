using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle;
using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using System.Data;
using OLM.Shared.Extensions.Pagination;

namespace OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle
{
    public class TCOBundleRepository : ITCOBundleRepository
    {
        private readonly IDbConnection _dbConnection;

        public TCOBundleRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<TCOBundleAPIResponseViewModel>> FetchData(DateTime from, DateTime to)
        {
            var sql = $"select * from TCOBundle where FinishedDate >= '{from}' and FinishedDate <= '{to}' order by ID";

            using (_dbConnection)
            {
                return await _dbConnection.QueryAsync<TCOBundleAPIResponseViewModel>(sql);
            }
        }

        public async Task<Paginated<TCOBundleAPIResponseViewModel>> FetchData(DateTime from, DateTime to, int pageIndex, int pageSize)
        {
            int skip = pageIndex * pageSize;
            int take = pageSize;
            var sql = $"select * from TCOBundle where FinishedDate >= '{from}' and FinishedDate <= '{to}' order by ID offset {skip} rows fetch next {take} rows only";
            var countSql = $"select count(*) from TCOBundle where FinishedDate >= '{from}' and FinishedDate <= '{to}'";

            using (_dbConnection)
            {
                var data = await _dbConnection.QueryAsync<TCOBundleAPIResponseViewModel>(sql);
                var count = await _dbConnection.QueryFirstAsync<int>(countSql);

                return new Paginated<TCOBundleAPIResponseViewModel>(pageIndex, pageSize, count, data);
            }
        }
    }
}
