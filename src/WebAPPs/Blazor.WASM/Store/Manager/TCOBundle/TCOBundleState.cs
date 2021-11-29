using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle
{
    public class TCOBundleState
    {
        public TCOBundleState(DateTime from,DateTime to, int pageIndex, int pageSize) : this(pageIndex, pageSize, true, from, to, default, default) { }

        public TCOBundleState(int pageIndex,
                                 int pageSize,
                                 bool isLoading,
                                 DateTime from, 
                                 DateTime to,
                                 APIError errors,
                                 Paginated<TCOBundleAPIResponseViewModel> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsLoading = isLoading;
            From = from;
            To = to;
            Errors = errors;
            Data = data;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool IsLoading { get; private set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public APIError Errors { get; private set; }

        public Paginated<TCOBundleAPIResponseViewModel> Data { get; private set; }
    }
}
