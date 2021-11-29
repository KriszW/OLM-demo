using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices
{
    public class BundlePricesState
    {
        public BundlePricesState(int pageIndex, int pageSize) : this(pageIndex, pageSize, true, default, default, default, default, default, default, default, default) { }

        public BundlePricesState(int pageIndex,
                                 int pageSize,
                                 bool isLoading,
                                 APIError errors,
                                 Paginated<BundlePriceDTOViewModel> data,
                                 BundlePriceDTOViewModel selectedModel,
                                 BundlePriceDTOViewModel modelForEdit,
                                 BundlePriceDTOViewModel selectedModelForDelete,
                                 bool uploadingFile,
                                 string uploadResponseMessage,
                                 List<string> currencies)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsLoading = isLoading;
            Errors = errors;
            SelectedModel = selectedModel;
            SelectedModelForDelete = selectedModelForDelete;
            ModelForEdit = modelForEdit;
            Data = data;
            UploadingFile = uploadingFile;
            UploadResponseMessage = uploadResponseMessage;
            Currencies = currencies;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool IsLoading { get; private set; }

        public APIError Errors { get; private set; }

        public BundlePriceDTOViewModel SelectedModel { get; private set; }
        public BundlePriceDTOViewModel SelectedModelForDelete { get; private set; }

        public BundlePriceDTOViewModel ModelForEdit { get; set; }

        public Paginated<BundlePriceDTOViewModel> Data { get; private set; }

        public bool UploadingFile { get; set; }
        public string UploadResponseMessage { get; set; }

        public List<string> Currencies { get; set; }
    }
}
