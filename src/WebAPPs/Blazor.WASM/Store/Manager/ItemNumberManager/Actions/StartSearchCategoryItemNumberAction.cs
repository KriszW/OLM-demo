using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions
{
    public class StartSearchCategoryItemNumberAction
    {
        public StartSearchCategoryItemNumberAction(string categoryQuery,
                                                   int pageIndex,
                                                   int pageSize)
        {
            CategoryQuery = categoryQuery;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public string CategoryQuery { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
