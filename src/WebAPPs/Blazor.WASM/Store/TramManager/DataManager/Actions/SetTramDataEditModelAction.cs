using OLM.Blazor.WASM.ViewModels.Tram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager.Actions
{
    public class SetTramDataEditModelAction
    {
        public SetTramDataEditModelAction(TramDataEditViewModel newModel)
        {
            NewModel = newModel;
        }

        public TramDataEditViewModel NewModel { get; set; }
    }
}
