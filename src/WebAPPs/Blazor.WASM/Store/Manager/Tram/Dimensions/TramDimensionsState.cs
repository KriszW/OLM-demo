using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions
{
    public class TramDimensionsState
    {
        public TramDimensionsState(int pageIndex, int pageSize) : this(pageIndex, pageSize, true,default,default,default,default, default) { }

        public TramDimensionsState(int pageIndex,
                                   int pageSize,
                                   bool isLoading,
                                   APIError errors,
                                   Paginated<TramDimensionViewModel> data,
                                   TramDimensionViewModel selectedModel,
                                   TramDimensionViewModel modelForEdit,
                                   TramDimensionViewModel selectedModelForDelete)
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

        public TramDimensionViewModel SelectedModel { get; private set; }
        public TramDimensionViewModel SelectedModelForDelete { get; private set; }

        public TramDimensionViewModel ModelForEdit { get; set; }

        public Paginated<TramDimensionViewModel> Data { get; private set; }
    }
}
