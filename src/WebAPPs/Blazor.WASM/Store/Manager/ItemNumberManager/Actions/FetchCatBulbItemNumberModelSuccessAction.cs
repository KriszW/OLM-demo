using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.CategoryBulbs.APIResponses.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions
{
    public class FetchCatBulbItemNumberModelSuccessAction
    {
        public FetchCatBulbItemNumberModelSuccessAction(Paginated<CategoryBulbItemNumberSettingsViewModel> data)
        {
            Data = data;
        }

        public Paginated<CategoryBulbItemNumberSettingsViewModel> Data { get; set; }
    }
}
