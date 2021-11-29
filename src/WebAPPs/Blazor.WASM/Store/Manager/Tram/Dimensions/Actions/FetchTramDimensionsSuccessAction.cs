using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions
{
    public class FetchTramDimensionsSuccessAction
    {
        public FetchTramDimensionsSuccessAction(Paginated<TramDimensionViewModel> model)
        {
            Model = model;
        }

        public Paginated<TramDimensionViewModel> Model { get; private set; }
    }
}
