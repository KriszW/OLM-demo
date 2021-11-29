using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Reducers
{
    public class ChangePriceModelReducer : Reducer<BundlePricesState, ChangePriceModelAction>
    {
        public override BundlePricesState Reduce(BundlePricesState state, ChangePriceModelAction action)
            => new BundlePricesState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     action.NewModelForEdit,
                                     default,
                                     state.UploadingFile,
                                     state.UploadResponseMessage,
                                     state.Currencies);
    }
}
