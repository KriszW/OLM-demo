using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.CategoryBulbs.APIResponses.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager
{
    public class CatBulbItemNumberManagerState
    {
        public CatBulbItemNumberManagerState(int pageIndex, int pageSize) : this(pageIndex, pageSize, true, default, default, default, default, default, default) { }

        public CatBulbItemNumberManagerState(int pageIndex,
                                 int pageSize,
                                 bool isLoading,
                                 string categorySearchQuery,
                                 APIError errors,
                                 Paginated<CategoryBulbItemNumberSettingsViewModel> data,
                                 CategoryBulbItemNumberSettingsViewModel selectedModel,
                                 CategoryBulbItemNumberSettingsViewModel modelForEdit,
                                 CategoryBulbItemNumberSettingsViewModel selectedModelForDelete)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsLoading = isLoading;
            CategorySearchQuery = categorySearchQuery;
            Errors = errors;
            Data = data;
            SelectedModel = selectedModel;
            ModelForEdit = modelForEdit;
            SelectedModelForDelete = selectedModelForDelete;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool IsLoading { get; private set; }

        public string CategorySearchQuery { get; set; }

        public APIError Errors { get; private set; }

        public CategoryBulbItemNumberSettingsViewModel SelectedModel { get; private set; }
        public CategoryBulbItemNumberSettingsViewModel SelectedModelForDelete { get; private set; }

        public CategoryBulbItemNumberSettingsViewModel ModelForEdit { get; set; }

        public Paginated<CategoryBulbItemNumberSettingsViewModel> Data { get; private set; }
    }
}
