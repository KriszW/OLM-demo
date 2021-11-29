using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class SelectedPriceModelChangedReducer : Reducer<BundlePricesState, SelectedPriceModelChangedAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, SelectedPriceModelChangedAction action)
            => new BundlePricesState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     action.NewSelectedModel,
                                     default,
                                     default,
                                     state.UploadingFile,
                                     state.UploadResponseMessage,
                                     state.Currencies);
    }
}
