using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes
{
    public class RoutingProductionTimePageState
    {
        public RoutingProductionTimePageState(int pageIndex, int pageSize) : this(pageIndex, pageSize, true, default, default) { }

        public RoutingProductionTimePageState(int pageIndex,
                                              int pageSize,
                                              bool isLoading,
                                              APIError errors,
                                              Paginated<WeekNumberPaginatorModelDataViewModel> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsLoading = isLoading;
            Errors = errors;
            Data = data;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool IsLoading { get; private set; }

        public APIError Errors { get; private set; }

        public Paginated<WeekNumberPaginatorModelDataViewModel> Data { get; private set; }

        public IEnumerable<ProductionTimeDataViewModel> ProdTimes { get; set; }
    }
}
