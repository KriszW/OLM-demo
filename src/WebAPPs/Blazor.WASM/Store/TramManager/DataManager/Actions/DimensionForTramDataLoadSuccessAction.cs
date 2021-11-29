using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager.Actions
{
    public class DimensionForTramDataLoadSuccessAction
    {
        public DimensionForTramDataLoadSuccessAction(IEnumerable<string> dimensions)
        {
            Dimensions = dimensions;
        }

        public IEnumerable<string> Dimensions { get; set; }
    }
}
