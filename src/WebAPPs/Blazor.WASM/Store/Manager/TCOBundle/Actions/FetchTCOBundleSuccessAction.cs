using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions
{
    public class FetchTCOBundleSuccessAction
    {
        public FetchTCOBundleSuccessAction(Paginated<TCOBundleAPIResponseViewModel> model)
        {
            Model = model;
        }

        public Paginated<TCOBundleAPIResponseViewModel> Model { get; private set; }
    }
}
