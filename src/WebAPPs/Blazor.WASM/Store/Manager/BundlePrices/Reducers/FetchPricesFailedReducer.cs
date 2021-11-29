using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class FetchPricesFailedReducer : Reducer<BundlePricesState, FetchBundlePricesFailedAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, FetchBundlePricesFailedAction action)
            => new BundlePricesState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     action.Errors,
                                     default,
                                     default,
                                     default,
                                     default,
                                     state.UploadingFile,
                                     state.UploadResponseMessage,
                                     state.Currencies);
    }
}
