using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class UploadBundlePriceFileFinishReducer : Reducer<BundlePricesState, UploadBundlePriceFileFinishedAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, UploadBundlePriceFileFinishedAction action)
             => new BundlePricesState(state.PageIndex,
                                      state.PageSize,
                                      state.IsLoading,
                                      state.Errors,
                                      state.Data,
                                      state.SelectedModel,
                                      state.SelectedModelForDelete,
                                      state.ModelForEdit,
                                      false,
                                      action.UploadMessage,
                                     state.Currencies);
    }
}
