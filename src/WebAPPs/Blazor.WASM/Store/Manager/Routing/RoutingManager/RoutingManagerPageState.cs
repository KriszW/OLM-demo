using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager
{
    public class RoutingManagerPageState
    {
        public RoutingManagerPageState(int pageIndex, int pageSize) : this(pageIndex, pageSize, true, default, default, default, default, default) { }

        public RoutingManagerPageState(int pageIndex,
                                       int pageSize,
                                       bool isLoading,
                                       APIError errors,
                                       Paginated<RoutingDataViewModel> data,
                                       RoutingDataViewModel selectedModel,
                                       RoutingDataViewModel modelForEdit,
                                       RoutingDataViewModel selectedModelForDelete)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsLoading = isLoading;
            Errors = errors;
            Data = data;
            SelectedModel = selectedModel;
            ModelForEdit = modelForEdit;
            SelectedModelForDelete = selectedModelForDelete;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool IsLoading { get; private set; }

        public APIError Errors { get; private set; }

        public RoutingDataViewModel SelectedModel { get; private set; }
        public RoutingDataViewModel SelectedModelForDelete { get; private set; }

        public RoutingDataViewModel ModelForEdit { get; set; }

        public Paginated<RoutingDataViewModel> Data { get; private set; }
    }
}
