using OLM.Shared.Models.CategoryBulbs.APIResponses.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions
{
    public class SelectedCatItemNumberModelChangedAction
    {
        public SelectedCatItemNumberModelChangedAction(CategoryBulbItemNumberSettingsViewModel newSelectedModel)
        {
            NewSelectedModel = newSelectedModel;
        }

        public CategoryBulbItemNumberSettingsViewModel NewSelectedModel { get; set; }
    }
}
