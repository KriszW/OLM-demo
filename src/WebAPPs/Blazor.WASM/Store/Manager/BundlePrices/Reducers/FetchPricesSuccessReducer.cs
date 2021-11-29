using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class FetchPricesSuccessReducer : Reducer<BundlePricesState, FetchBundlePricesSuccessAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, FetchBundlePricesSuccessAction action)
            => new BundlePricesState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     default,
                                     action.Model,
                                     default,
                                     default,
                                     default,
                                     state.UploadingFile,
                                     state.UploadResponseMessage,
                                     state.Currencies);
    }
}
