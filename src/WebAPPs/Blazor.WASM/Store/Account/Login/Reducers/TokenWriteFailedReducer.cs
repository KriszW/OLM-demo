using Fluxor;
using OLM.Blazor.WASM.Store.Account.Login.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Reducers
{
    public class TokenWriteFailedReducer : Reducer<LoginState, TokenWriteFailedAction>
    {
        public override LoginState Reduce(LoginState state, TokenWriteFailedAction action)
            => new LoginState(action.ErrorMSG, state.ReturnURL, false);
    }
}
