using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.TCOSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions
{
    public class FetchSettingsModelSuccessAction
    {
        public FetchSettingsModelSuccessAction(Paginated<TCOSettingsViewModel> data)
        {
            Data = data;
        }

        public Paginated<TCOSettingsViewModel> Data { get; set; }
    }
}
