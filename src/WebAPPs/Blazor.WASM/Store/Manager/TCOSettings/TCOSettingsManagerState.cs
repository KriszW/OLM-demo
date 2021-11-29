using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.TCOSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings
{
    public class TCOSettingsManagerState
    {
        public TCOSettingsManagerState(int pageIndex, int pageSize) : this(pageIndex, pageSize, true, default, default, default, default, default) { }

        public TCOSettingsManagerState(int pageIndex,
                                 int pageSize,
                                 bool isLoading,
                                 APIError errors,
                                 Paginated<TCOSettingsViewModel> data,
                                 TCOSettingsViewModel selectedModel,
                                 TCOSettingsViewModel modelForEdit,
                                 TCOSettingsViewModel selectedModelForDelete)
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

        public TCOSettingsViewModel SelectedModel { get; private set; }
        public TCOSettingsViewModel SelectedModelForDelete { get; private set; }

        public TCOSettingsViewModel ModelForEdit { get; set; }

        public Paginated<TCOSettingsViewModel> Data { get; private set; }
    }
}
