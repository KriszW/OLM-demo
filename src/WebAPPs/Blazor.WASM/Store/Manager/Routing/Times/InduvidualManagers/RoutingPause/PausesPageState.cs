using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause
{
    public class PausesPageState
    {
        public PausesPageState(int pageIndex, int pageSize) : this(pageIndex, pageSize, true,default,default,default,default, default) { }

        public PausesPageState(int pageIndex,
                                   int pageSize,
                                   bool isLoading,
                                   APIError errors,
                                   Paginated<PauseDataViewModel> data,
                                   PauseDataViewModel selectedModel,
                                   PauseDataViewModel modelForEdit,
                                   PauseDataViewModel selectedModelForDelete)
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

        public PauseDataViewModel SelectedModel { get; private set; }
        public PauseDataViewModel SelectedModelForDelete { get; private set; }

        public PauseDataViewModel ModelForEdit { get; set; }

        public Paginated<PauseDataViewModel> Data { get; private set; }

        public DateTime StartForModifyLocalized
        {
            get
            {
                if (ModelForEdit != default && ModelForEdit.Start != default)
                {
                    var date = ModelForEdit.Start.ToUniversalTime();

                    return date;
                }
                else
                {
                    return default;
                }
            }

            set
            {
                if (ModelForEdit != default)
                {
                    var oldDate = ModelForEdit.Start;
                    var newValue = value.ToLocalTime();

                    var diff = newValue - oldDate;

                    var newDate = ModelForEdit.Start.Add(diff);

                    ModelForEdit.Start = newDate;
                }
            }
        }

        public DateTime EndForModifyLocalized
        {
            get
            {
                if (ModelForEdit != default && ModelForEdit.End != default)
                {
                    var date = ModelForEdit.End.ToUniversalTime();

                    return date;
                }
                else
                {
                    return default;
                }
            }

            set
            {
                if (ModelForEdit != default)
                {
                    var oldDate = ModelForEdit.End;
                    var newValue = value.ToLocalTime();

                    var diff = newValue - oldDate;

                    var newDate = ModelForEdit.End.Add(diff);

                    ModelForEdit.End = newDate;
                }
            }
        }
    }
}
