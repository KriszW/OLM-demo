using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using OLM.Shared.Models.Target.SharedAPIModels;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency
{
    public class CurrencyManagerState
    {
        public CurrencyManagerState(int pageIndex, int pageSize) : this(pageIndex, pageSize, true,default,default,default,default, default) { }

        public CurrencyManagerState(int pageIndex,
                                   int pageSize,
                                   bool isLoading,
                                   APIError errors,
                                   Paginated<CurrencyViewModel> data,
                                   CurrencyViewModel selectedModel,
                                   CurrencyViewModel modelForEdit,
                                   CurrencyViewModel selectedModelForDelete)
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

        public CurrencyViewModel SelectedModel { get; private set; }
        public CurrencyViewModel SelectedModelForDelete { get; private set; }

        public CurrencyViewModel ModelForEdit { get; set; }

        public Paginated<CurrencyViewModel> Data { get; private set; }
    }
}
