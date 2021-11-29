using OLM.Shared.Models.CategoryBulbs.APIResponses.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions
{
    public class EditCatBulbItemNumberModelAction
    {
        public EditCatBulbItemNumberModelAction(CategoryBulbItemNumberSettingsViewModel model)
        {
            Model = model;
        }

        public CategoryBulbItemNumberSettingsViewModel Model { get; set; }
    }
}
