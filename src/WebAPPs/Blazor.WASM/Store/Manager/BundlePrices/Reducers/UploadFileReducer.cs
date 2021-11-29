using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class UploadFileReducer : Reducer<BundlePricesState, UploadPowerBiFileWithCurrenciesAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, UploadPowerBiFileWithCurrenciesAction action)
            => new BundlePricesState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     state.SelectedModelForDelete,
                                     state.ModelForEdit,
                                     true,
                                     default,
                                     state.Currencies);
    }
}
