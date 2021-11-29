using OLM.Shared.Models.TCO.SharedAPIModels.TCO.TCOSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions
{
    public class EditTCOSettingsModelAction
    {
        public EditTCOSettingsModelAction(TCOSettingsViewModel modelForEdit)
        {
            ModelForEdit = modelForEdit;
        }

        public TCOSettingsViewModel ModelForEdit { get; set; }
    }
}
