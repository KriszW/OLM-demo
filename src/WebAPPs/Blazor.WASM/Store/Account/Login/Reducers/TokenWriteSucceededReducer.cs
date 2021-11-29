using Fluxor;
using OLM.Blazor.WASM.Store.Account.Login.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Reducers
{
    public class TokenWriteSucceededReducer : Reducer<LoginState, TokenWriteSucceededAction>
    {
        public override LoginState Reduce(LoginState state, TokenWriteSucceededAction action)
            => new LoginState(default, state.ReturnURL, true);
    }
}
