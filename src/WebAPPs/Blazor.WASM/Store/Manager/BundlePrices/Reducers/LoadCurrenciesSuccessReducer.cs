using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class LoadCurrenciesSuccessReducer : Reducer<BundlePricesState, LoadCurrenciesSuccessAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, LoadCurrenciesSuccessAction action)
            => new BundlePricesState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     state.SelectedModelForDelete,
                                     state.ModelForEdit,
                                     state.UploadingFile,
                                     state.UploadResponseMessage,
                                     action.Currencies);
    }
}
