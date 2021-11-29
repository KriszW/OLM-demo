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

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates
{
    public class ExchangeRateManagerState
    {
        public ExchangeRateManagerState(string isoCode,
                                        int pageIndex,
                                        int pageSize) : this(isoCode,
                                                             pageIndex,
                                                             pageSize,
                                                             true,
                                                             default,
                                                             default,
                                                             default,
                                                             default,
                                                             default) 
        { }

        public ExchangeRateManagerState(string iSOCode,
                                        int pageIndex,
                                        int pageSize,
                                        bool isLoading,
                                        APIError errors,
                                        Paginated<ExchangeRateViewModel> data,
                                        ExchangeRateViewModel selectedModel,
                                        ExchangeRateViewModel modelForEdit,
                                        ExchangeRateViewModel selectedModelForDelete)
        {
            ISOCode = iSOCode;
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsLoading = isLoading;
            Errors = errors;
            SelectedModel = selectedModel;
            SelectedModelForDelete = selectedModelForDelete;
            ModelForEdit = modelForEdit;
            Data = data;
        }

        public string ISOCode { get; set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool IsLoading { get; private set; }

        public APIError Errors { get; private set; }

        public Paginated<ExchangeRateViewModel> Data { get; private set; }

        public ExchangeRateViewModel SelectedModel { get; private set; }
        public ExchangeRateViewModel SelectedModelForDelete { get; private set; }

        public ExchangeRateViewModel ModelForEdit { get; set; }
    }
}
