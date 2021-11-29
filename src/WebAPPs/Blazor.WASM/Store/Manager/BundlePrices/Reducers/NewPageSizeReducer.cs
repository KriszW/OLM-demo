using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class NewPageSizeReducer : Reducer<BundlePricesState, PageSizeChangedAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, PageSizeChangedAction action)
            => new BundlePricesState(state.PageIndex,
                                     action.NewPageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default,
                                     state.UploadingFile,
                                     state.UploadResponseMessage,
                                     state.Currencies);
    }
}
