using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Shared.Extensions.Pagination
{
    public class Paginated<TModel>
        where TModel : class
    {
        [JsonConstructor]
        public Paginated(int actualPage, int pageSize, long totalItemCount, IEnumerable<TModel> data)
        {
            ActualPage = actualPage;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            Data = data;
        }

        public int ActualPage { get; set; }
        public int PageSize { get; set; }
        public long TotalItemCount { get; set; }

        public IEnumerable<TModel> Data { get; set; }
    }
}
