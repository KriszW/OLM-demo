using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class SetDeletingModelReducer : Reducer<BundlePricesState, ModelForDeletingSetAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, ModelForDeletingSetAction action)
            => new BundlePricesState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     state.ModelForEdit,
                                     action.Model,
                                     state.UploadingFile,
                                     state.UploadResponseMessage,
                                     state.Currencies);
    }
}
